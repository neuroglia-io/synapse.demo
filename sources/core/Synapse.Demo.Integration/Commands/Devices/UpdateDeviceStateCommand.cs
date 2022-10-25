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
/// Represents the command DTO used to update the state of a <see cref="Device"/>
/// </summary>
[CloudEventEnvelope("device", "update-state")]
public class UpdateDeviceStateCommand
    : IIntegrationCommand
{
    /// <summary>
    /// Gets the id of the <see cref="Device"/> to create
    /// </summary>
    public string DeviceId { get; init; }

    /// <summary>
    /// Gets the state of the <see cref="Device"/> to create
    /// </summary>
    public object? State { get; init; }

    /// <summary>
    /// Initializes a new <see cref="UpdateDeviceStateCommand"/>
    /// </summary>
    /// <param name="deviceId">The id of the <see cref="Device"/> to change the state of</param>
    /// <param name="state">The new state of the <see cref="Device"/></param>
    public UpdateDeviceStateCommand(string deviceId, object? state)
    {
        if (string.IsNullOrWhiteSpace(deviceId)) throw DomainException.ArgumentNullOrWhitespace(nameof(deviceId));
        this.DeviceId = deviceId;
        this.State = state;
    }
}
