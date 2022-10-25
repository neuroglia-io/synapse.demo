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

namespace Synapse.Demo.Application.Commands.Devices;

// TODO: Write tests
/// <summary>
/// Represents a <see cref="ICommandHandler"/> used to handle the <see cref="PatchDeviceStateCommand"/>
/// </summary>
internal class PatchDeviceStateCommandHandler
    : CommandHandlerBase, ICommandHandler<PatchDeviceStateCommand, Device>
{
    /// <summary>
    /// Gets the <see cref="IRepository"/> used to manage <see cref="Domain.Models.Device"/>s
    /// </summary>
    protected IRepository<Domain.Models.Device> Devices { get; init; }

    /// <summary>
    /// Gets the <see cref="IJsonSerializer"/> used to clone the original state
    /// </summary>
    protected IJsonSerializer Serializer { get; set; }

    /// <summary>
    /// Initializes a new <see cref="PatchDeviceStateCommandHandler"/>
    /// </summary>
    /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
    /// <param name="mediator">The service used to mediate calls</param>
    /// <param name="mapper">The service used to map objects</param>
    /// <param name="devices">The <see cref="IRepository"/> used to manage <see cref="Domain.Models.Device"/>s</param>
    /// <param name="serializer">The <see cref="IJsonSerializer"/></param>
    public PatchDeviceStateCommandHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper
        , IRepository<Domain.Models.Device> devices, IJsonSerializer serializer) 
        : base(loggerFactory, mediator, mapper)
    {
        this.Devices = devices;
        this.Serializer = serializer;
    }

    /// <inheritdoc/>
    public async Task<IOperationResult<Device>> HandleAsync(PatchDeviceStateCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var device = await this.Devices.FindAsync(command.DeviceId, cancellationToken);
            if (device == null) throw DomainException.NullReference(typeof(Device), command.DeviceId);
            dynamic? newState;
            if (command.State == null)
            {
                newState = null;
            }
            else if (device.State == null)
            {
                newState = command.State;
            }
            else
            {
                newState = device.State.Merge((command.State as JObject).ToObject());
            }
            device.SetState(newState);
            device = await this.Devices.UpdateAsync(device, cancellationToken);
            await this.Devices.SaveChangesAsync(cancellationToken);
            var deviceDto = this.Mapper.Map<Device>(device);
            return this.Ok(deviceDto);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
