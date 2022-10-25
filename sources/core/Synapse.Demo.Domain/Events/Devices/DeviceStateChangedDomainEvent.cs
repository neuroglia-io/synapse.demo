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

namespace Synapse.Demo.Domain.Events.Devices;

/// <summary>
/// The <see cref="DomainEvent"/> fired after the state of a <see cref="Device"/> as changed
/// </summary>
[DataTransferObjectType(typeof(DeviceStateChangedIntegrationEvent))]
public class DeviceStateChangedDomainEvent
    : DomainEvent<Device>
{

    /// <summary>
    /// Gets the state of the <see cref="Device"/>
    /// </summary>
    public object? State { get; protected set; } = null!;

    /// <summary>
    /// Initializes a new <see cref="DeviceCreatedDomainEvent"/>
    /// </summary>
    protected DeviceStateChangedDomainEvent()
        : base(null!)
    {}

    /// <summary>
    /// Initializes a new <see cref="DeviceCreatedDomainEvent"/>
    /// </summary>
    /// <param name="id">The id of the <see cref="Device"/> which's state has changed</param>
    /// <param name="state">The new state of the <see cref="Device"/></param>
    public DeviceStateChangedDomainEvent(string id, object? state)
        : base(id)
    {
        this.State = state;
    }
}
