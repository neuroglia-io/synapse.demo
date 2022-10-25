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

namespace Synapse.Demo.Integration.Events.Devices;

// TODO: Write tests
/// <summary>
/// The integration event fired after the creation of a <see cref="Domain.Models.Device"/>
/// </summary>
[CloudEventEnvelope("device", "created")]
public class DeviceCreatedIntegrationEvent
    : IntegrationEvent
{
    /// <summary>
    /// Gets the id of the created <see cref="Device"/>
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Gets the label of the created <see cref="Device"/>
    /// </summary>
    public string Label { get; init; }

    /// <summary>
    /// Gets the type of <see created cref="Device"/>
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Gets the location of the created <see cref="Device"/> 
    /// </summary>
    public Location Location { get; init; }

    /// <summary>
    /// Gets the state of the created <see cref="Device"/>
    /// </summary>
    public object? State { get; init; }

    /// <summary>
    /// Initializes a new <see cref="DeviceCreatedIntegrationEvent"/>
    /// </summary>
    /// <param name="id">The id of the created <see cref="Device"/></param>
    /// <param name="label">The label of the created <see cref="Device"/></param>
    /// <param name="type">The type of the created <see cref="Device"/></param>
    /// <param name="location">The location of the created <see cref="Device"/></param>
    /// <param name="state">The state of the created <see cref="Device"/></param>
    public DeviceCreatedIntegrationEvent(string id, string label, string type, Location location, object? state)
    {
        this.AggregateId = id;
        this.CreatedAt = DateTime.UtcNow;
        this.Id = id;
        this.Label = label;
        this.Type = type;
        this.Location = location;
        this.State = state;
    }

    /// <summary>
    /// Initializes a new <see cref="DeviceCreatedIntegrationEvent"/>
    /// </summary>
    protected DeviceCreatedIntegrationEvent() { }
}
