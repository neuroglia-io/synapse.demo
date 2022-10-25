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
/// Represents a <see cref="Device"/> of type `sensor.thermometer`
/// </summary>
public class Thermometer
    : DeviceBase
{
    /// <summary>
    /// Gets the measured temperature
    /// </summary>
    public int? Temperature { get; set; }
    /// <summary>
    /// Gets the measured temperature as string
    /// </summary>
    public string DisplayedTemperature => this.Temperature != null ? $"{this.Temperature}°C" : "N/A";
    /// <summary>
    /// Gets the desired temperature
    /// </summary>
    public int? DesiredTemperature { get; set; }
    /// <summary>
    /// Gets the desired temperature as string
    /// </summary>
    public string DisplayedDesiredTemperature => this.DesiredTemperature != null ? $"{this.DesiredTemperature}°C" : "N/A";
}
