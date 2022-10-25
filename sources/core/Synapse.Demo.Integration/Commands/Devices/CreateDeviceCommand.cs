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

namespace Synapse.Demo.Integration.Commands.Devices;

// TODO: Write tests
/// <summary>
/// Represents the command DTO used to create a new <see cref="Device"/>
/// </summary>
[CloudEventEnvelope("device", "create")]
public class CreateDeviceCommand
    : IIntegrationCommand
{
    /// <summary>
    /// Gets the id of the <see cref="Device"/> to create
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Gets the label of the <see cref="Device"/> to create
    /// </summary>
    public string Label { get; init; }

    /// <summary>
    /// Gets the type of <see cref="Device"/> to create
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Gets the location of the <see cref="Device"/> to create
    /// </summary>
    public string Location { get; init; }

    /// <summary>
    /// Gets the state of the <see cref="Device"/> to create
    /// </summary>
    public object? State { get; init; }

    /// <summary>
    /// Initializes a new <see cref="CreateDeviceCommand"></see>
    /// </summary>
    /// <param name="id">The id of the <see cref="Device"/> to create</param>
    /// <param name="label">The label of the <see cref="Device"/> to create</param>
    /// <param name="type">The type of <see cref="Device"/> to create</param>
    /// <param name="location">The location of the <see cref="Device"/> to create</param>
    /// <param name="state">The state of the <see cref="Device"/> to create</param>
    public CreateDeviceCommand(string id, string label, string type, string location, object? state)
    {
        if (string.IsNullOrWhiteSpace(id)) throw DomainException.ArgumentNullOrWhitespace(nameof(id));
        if (string.IsNullOrWhiteSpace(label)) throw DomainException.ArgumentNullOrWhitespace(nameof(label));
        if (string.IsNullOrWhiteSpace(type)) throw DomainException.ArgumentNullOrWhitespace(nameof(type));
        if (string.IsNullOrWhiteSpace(location)) throw DomainException.ArgumentNullOrWhitespace(nameof(location));
        this.Id = id;
        this.Label = label;
        this.Type = type;
        this.Location = location;
        this.State = state;
    }
}
