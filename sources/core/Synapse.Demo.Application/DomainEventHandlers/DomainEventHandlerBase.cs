﻿// Copyright © 2022-Present The Synapse Authors. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.Options;
using Synapse.Demo.Application.Configuration;
using System.Net.Mime;

namespace Synapse.Demo.Application.DomainEventHandlers;

/// <summary>
/// Represents the base class for all <see cref="INotificationHandler{TNotification}"/> used to handle <see cref="IDomainEvent"/>s
/// </summary>
internal class DomainEventHandlerBase
{
    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; init; }

    /// <summary>
    /// Gets the applicaiton <see cref="IOptions{TOptions}"/>
    /// </summary>
    protected DemoApplicationOptions Options { get; init; }

    /// <summary>
    /// Gets the service used to map objects
    /// </summary>
    protected IMapper Mapper { get; init; }

    /// <summary>
    /// Gets the service used to mediate calls
    /// </summary>
    protected IMediator Mediator { get; init; }

    /// <summary>
    /// Gets the service used to publish and subscribe to <see cref="CloudEvent"/>s
    /// </summary>
    protected ICloudEventBus CloudEventBus { get; init; }

    /// <summary>
    /// Gets the <see cref="Subject{T}"/> used to observe consumed <see cref="CloudEvent"/>s
    /// </summary>
    protected ISubject<CloudEvent> CloudEventStream { get; init; }

    /// <summary>
    /// Initializes a new <see cref="DomainEventHandlerBase"/>
    /// </summary>
    /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
    /// <param name="options">The applicaiton <see cref="IOptions{TOptions}"/></param>
    /// <param name="mapper">The service used to map objects</param>
    /// <param name="mediator">The service used to mediate calls</param>
    /// <param name="cloudEventBus">The service used to publish and subscribe to <see cref="CloudEvent"/>s</param>
    /// <param name="cloudEventBus">The <see cref="Subject{T}"/> used to observe consumed <see cref="CloudEvent"/>s</param>
    protected DomainEventHandlerBase(ILoggerFactory loggerFactory, IOptions<DemoApplicationOptions> options, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus, ISubject<CloudEvent> cloudEventStream)
    {
        if (loggerFactory == null) throw DomainException.ArgumentNull(nameof(loggerFactory));
        if (options == null) throw DomainException.ArgumentNull(nameof(options));
        if (mapper == null) throw DomainException.ArgumentNull(nameof(mapper));
        if (mediator == null) throw DomainException.ArgumentNull(nameof(mediator));
        if (cloudEventBus == null) throw DomainException.ArgumentNull(nameof(cloudEventBus));
        if (cloudEventStream == null) throw DomainException.ArgumentNull(nameof(cloudEventStream));
        this.Logger = loggerFactory.CreateLogger(this.GetType());
        this.Options = options.Value;
        this.Mapper = mapper;
        this.Mediator = mediator;
        this.CloudEventBus = cloudEventBus;
        this.CloudEventStream = cloudEventStream;
    }

    /// <summary>
    /// Publishes the specified a <see cref="IntegrationEvent"/> on the <see cref="IMediator"/> and the <see cref="ICloudEventBus"/>
    /// </summary>
    /// <param name="e">The <see cref="IntegrationEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual async Task PublishIntegrationEventAsync<TEvent>(TEvent e, CancellationToken cancellationToken)
        where TEvent : class, Integration.IIntegrationEvent
    {
        await this.Mediator.PublishAsync(e);
        if (!e.GetType().TryGetCustomAttribute(out CloudEventEnvelopeAttribute cloudEventEnvelopeAttribute))
            return;
        var eventIdentifier = $"{cloudEventEnvelopeAttribute.AggregateType}/{cloudEventEnvelopeAttribute.ActionName}/v1";
        CloudEvent cloudEvent = new()
        {
            Id = Guid.NewGuid().ToString(),
            Source = new (this.Options.CloudEventsSource),
            Type = $"{ApplicationConstants.CloudEventsType}/{eventIdentifier}",
            Time = e.CreatedAt,
            Subject = e.AggregateId.ToString(),
            DataSchema = new($"{this.Options.SchemaRegistry}/{eventIdentifier}", UriKind.RelativeOrAbsolute),
            DataContentType = MediaTypeNames.Application.Json,
            Data = e
        };
        await this.CloudEventBus.PublishAsync(cloudEvent, cancellationToken);
        this.CloudEventStream.OnNext(cloudEvent);
    }
}

/// <summary>
/// Represents the base class for all <see cref="INotificationHandler{TNotification}"/> used to handle <see cref="IDomainEvent"/>s
/// </summary>
/// <typeparam name="TWriteModel">The type of the write model for which to handle <see cref="IDomainEvent"/>s</typeparam>
/// <typeparam name="TReadModel">The type of the read model to project handled <see cref="IDomainEvent"/>s to</typeparam>
/// <typeparam name="TKey">The type of key used to uniquely identify the read and write models</typeparam>
internal abstract class DomainEventHandlerBase<TWriteModel, TReadModel, TKey>
    : DomainEventHandlerBase
    where TWriteModel : class, IAggregateRoot<TKey>
    where TReadModel : class, IIdentifiable<TKey>
    where TKey : IEquatable<TKey>
{

    /// <summary>
    /// Gets the <see cref="IRepository"/> used to manage the write models for which to handle <see cref="IDomainEvent"/>s
    /// </summary>
    protected IRepository<TWriteModel, TKey> WriteModels { get; init; }

    /// <summary>
    /// Gets the <see cref="IRepository"/> used to manage the read models to project handled <see cref="IDomainEvent"/>s to
    /// </summary>
    protected IRepository<TReadModel, TKey> ReadModels { get; init; }

    /// <summary>
    /// Initializes a new <see cref="DomainEventHandlerBase"/>
    /// </summary>
    /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
    /// <param name="options">The applicaiton <see cref="IOptions{TOptions}"/></param>
    /// <param name="mapper">The service used to map objects</param>
    /// <param name="mediator">The service used to mediate calls</param>
    /// <param name="cloudEventBus">The service used to publish and subscribe to <see cref="CloudEvent"/>s</param>
    /// <param name="cloudEventBus">The <see cref="Subject{T}"/> used to observe consumed <see cref="CloudEvent"/>s</param>
    /// <param name="writeModels">The <see cref="IRepository"/> used to manage the write models for which to handle <see cref="IDomainEvent"/>s</param>
    /// <param name="readModels">The <see cref="IRepository"/> used to manage the read models to project handled <see cref="IDomainEvent"/>s to</param>
    protected DomainEventHandlerBase(ILoggerFactory loggerFactory, IOptions<DemoApplicationOptions> options, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus, ISubject<CloudEvent> cloudEventStream,
        IRepository<TWriteModel, TKey> writeModels, IRepository<TReadModel, TKey> readModels)
        : base(loggerFactory, options, mapper, mediator, cloudEventBus, cloudEventStream)
    {
        if (writeModels == null) throw DomainException.ArgumentNull(nameof(writeModels));
        if (readModels == null) throw DomainException.ArgumentNull(nameof(readModels));
        this.WriteModels = writeModels;
        this.ReadModels = readModels;
    }

    /// <summary>
    /// Gets or reconciles the read model for the <see cref="IAggregateRoot"/> with the specified key
    /// </summary>
    /// <param name="aggregateKey">The id of the <see cref="IAggregateRoot"/> to get the read model for</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The read model for the <see cref="IAggregateRoot"/> with the specified key</returns>
    protected virtual async Task<TReadModel> GetOrReconcileReadModelForAsync(TKey aggregateKey, CancellationToken cancellationToken)
    {
        TReadModel readModel = await this.ReadModels.FindAsync(aggregateKey, cancellationToken);
        if (readModel == null)
        {
            TWriteModel writeModel = await this.WriteModels.FindAsync(aggregateKey, cancellationToken);
            if (writeModel == null)
            {
                this.Logger.LogError("Failed to find a aggregate of type {aggregateType} with the specified key {key}", typeof(TWriteModel), aggregateKey);
                throw new Exception($"Failed to find a aggregate of type {typeof(TWriteModel)} with the specified key {aggregateKey}");
            }
            readModel = await this.ProjectAsync(writeModel, cancellationToken);
            readModel = await this.ReadModels.AddAsync(readModel, cancellationToken);
            await this.ReadModels.SaveChangesAsync(cancellationToken);
        }
        return readModel;
    }

    /// <summary>
    /// Projects the specified <see cref="IAggregateRoot"/>
    /// </summary>
    /// <param name="writeModel">The <see cref="IAggregateRoot"/> to project</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The projected <see cref="IAggregateRoot"/></returns>
    protected virtual async Task<TReadModel> ProjectAsync(TWriteModel writeModel, CancellationToken cancellationToken)
    {
        return await Task.FromResult(this.Mapper.Map<TReadModel>(writeModel));
    }

}