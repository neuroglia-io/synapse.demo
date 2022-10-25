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

namespace Synapse.Demo.Application.Services;

/// <summary>
/// Defines the fundamentals of a service used to interact with AC systems
/// </summary>
public interface IAirConditioner
{

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="IAirConditioner"/> is powered on
    /// </summary>
    bool IsPoweredOn { get; }

    /// <summary>
    /// Powers the AC on
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task TurnOnAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Powers the AC off
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task TurnOffAsync(CancellationToken cancellationToken = default);

}
