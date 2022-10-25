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

namespace Synapse.Demo.WebUI;

/// <summary>
/// Holds the data to present a <see cref="Device"/> in a <see cref="DeviceWidget"/> component
/// </summary>
public class DeviceWidgetViewModel
{
    /// <summary>
    /// Gets the id of the device
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets the label of the device
    /// </summary>
    public string Label { get; set; } = null!;

    /// <summary>
    /// Gets the data to display
    /// </summary>
    public string Data { get; set; } = null!;

    /// <summary>
    /// Gets the content of the hero header
    /// </summary>
    public object? Hero { get; set; } = null;

    /// <summary>
    /// Gets if the device is active
    /// </summary>
    public bool IsActive { get; set; } = false;
}
