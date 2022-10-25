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

namespace Synapse.Demo.Integration.Models;

/// <summary>
/// Represents an IoT device
/// </summary>
[Queryable]
public class Device
    : DeviceBase
{
    /// <summary>
    /// Gets/Sets the label of the <see cref="Device"/>
    /// </summary>
    public string Label { get; set; } = null!;

    /// <summary>
    /// Gets/Sets the type of <see cref="Device"/>
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Gets/Sets the location of the <see cref="Device"/>
    /// </summary>
    public Location Location { get; set; } = null!;

    /// <summary>
    /// Gets/Sets the state of the <see cref="Device"/>
    /// </summary>
    public object? State { get; set; } = null!;
}