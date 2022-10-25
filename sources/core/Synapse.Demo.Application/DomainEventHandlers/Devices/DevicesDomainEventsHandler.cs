// Copyright © 2022-Present The Synapse Authors. All rights reserved.
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

namespace Synapse.Demo.Application.DomainEventHandlers.Devices;

// TODO: Write tests
/// <summary>
/// Handles <see cref="IDomainEvent"/>s related to <see cref="Domain.Models.Device"/>s
/// </summary>
internal class DevicesDomainEventsHandler
    : DomainEventHandlerBase<Domain.Models.Device, Device, string>
    , INotificationHandler<DeviceCreatedDomainEvent>
    , INotificationHandler<DeviceStateChangedDomainEvent>
{
    /// <inheritdoc/>
    public DevicesDomainEventsHandler(ILoggerFactory loggerFactory, IOptions<DemoApplicationOptions> options, IMapper mapper, IMediator mediator, ICloudEventBus cloudEventBus, ISubject<CloudEvent> cloudEventStream, IRepository<Domain.Models.Device, string> writeModels, IRepository<Device, string> readModels) 
        : base(loggerFactory, options, mapper, mediator, cloudEventBus, cloudEventStream, writeModels, readModels)
    {}

    /// <summary>
    /// Handles a <see cref="DeviceCreatedDomainEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="DeviceCreatedDomainEvent"/> to handle</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task HandleAsync(DeviceCreatedDomainEvent e, CancellationToken cancellationToken = default)
    {
        await this.GetOrReconcileReadModelForAsync(e.AggregateId, cancellationToken);
        var integrationEvent = this.Mapper.Map<DeviceCreatedIntegrationEvent>(e);
        await this.PublishIntegrationEventAsync(integrationEvent, cancellationToken);
    }

    /// <summary>
    /// Handles a <see cref="DeviceStateChangedDomainEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="DeviceCreatedDomainEvent"/> to handle</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task HandleAsync(DeviceStateChangedDomainEvent e, CancellationToken cancellationToken = default)
    {
        var device = await this.GetOrReconcileReadModelForAsync(e.AggregateId, cancellationToken);
        device.State = e.State;
        await this.ReadModels.UpdateAsync(device, cancellationToken);
        await this.ReadModels.SaveChangesAsync(cancellationToken);
        var integrationEvent = this.Mapper.Map<DeviceStateChangedIntegrationEvent>(e);
        await this.PublishIntegrationEventAsync(integrationEvent, cancellationToken);
    }
}
