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
/// The <see cref="DomainEvent"/> fired after the creation of a <see cref="Device"/>
/// </summary>
[DataTransferObjectType(typeof(DeviceCreatedIntegrationEvent))]
public class DeviceCreatedDomainEvent
    : DomainEvent<Device>
{
    /// <summary>
    /// Gets the id of the <see cref="Device"/>
    /// </summary>
    public string Id { get; protected set; } = null!;

    /// <summary>
    /// Gets the label of the <see cref="Device"/>
    /// </summary>
    public string Label { get; protected set; } = null!;

    /// <summary>
    /// Gets the type of <see cref="Device"/>
    /// </summary>
    public string Type { get; protected set; } = null!;

    /// <summary>
    /// Gets the location of the <see cref="Device"/>
    /// </summary>
    public string Location { get; protected set; } = null!;

    /// <summary>
    /// Gets the state of the <see cref="Device"/>
    /// </summary>
    public object? State { get; protected set; } = null!;

    /// <summary>
    /// Initializes a new <see cref="DeviceCreatedDomainEvent"/>
    /// </summary>
    protected DeviceCreatedDomainEvent()
        : base(null!)
    {}

    /// <summary>
    /// Initializes a new <see cref="DeviceCreatedDomainEvent"/>
    /// </summary>
    /// <param name="id">The id of the newly created <see cref="Device"/></param>
    /// <param name="label">The label of the created <see cref="Device"/></param>
    /// <param name="type">The type of the created <see cref="Device"/></param>
    /// <param name="location">The location of the created <see cref="Device"/></param>
    /// <param name="state">The state of the create <see cref="Device"/></param>
    public DeviceCreatedDomainEvent(string id, string label, string type, string location, object? state)
        : base(id)
    {
        this.Id = id;
        this.Label = label;
        this.Type = type;
        this.Location = location;
        this.State = state;
    }
}
