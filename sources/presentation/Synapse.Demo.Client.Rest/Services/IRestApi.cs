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

using System.Threading.Tasks;

namespace Synapse.Demo.Client.Rest.Services;

/// <summary>
/// Represents the service used to interact with the HTTP REST API
/// </summary>
public interface IRestApiClient
{
    /// <summary>
    /// Creates a new <see cref="Device"/>
    /// </summary>
    /// <param name="command">The <see cref="CreateDeviceCommand"/> used to create the device</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The created <see cref="Device"/></returns>
    Task<Device> CreateDevice(CreateDeviceCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries the <see cref="Device"/>s
    /// </summary>
    /// <param name="query">The potential OData query</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A list of <see cref="Device"/>s</returns>
    Task<IEnumerable<Device>> GetDevices(string? query = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="Device"/> with the specified id.
    /// </summary>
    /// <param name="id">The id of the <see cref="Device"/> to find</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The targeted <see cref="Device"/></returns>
    Task<Device> GetDeviceById(string id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a <see cref="Device"/> state
    /// </summary>
    /// <param name="command">The <see cref="UpdateDeviceStateCommand"/> used to update the device state</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="Device"/></returns>
    Task<Device> UpdateDeviceState(UpdateDeviceStateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Patch a <see cref="Device"/> state
    /// </summary>
    /// <param name="command">The <see cref="PatchDeviceStateCommand"/> used to patch the device state</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="Device"/></returns>
    Task<Device> PatchDeviceState(PatchDeviceStateCommand command, CancellationToken cancellationToken = default);
}