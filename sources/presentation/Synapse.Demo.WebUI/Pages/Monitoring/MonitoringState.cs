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

namespace Synapse.Demo.WebUI.Pages.Monitoring.State;

/// <summary>
/// The <see cref="State{TState}"/> of the monitoring page
/// </summary>
[Feature]
public record MonitoringState
{
    /// <summary>
    /// Gets the <see cref="Device"/>s displayed
    /// </summary>
    public Dictionary<string, Device> Devices { get; set; } = new Dictionary<string, Device>();

    /// <summary>
    /// Gets the visibility state of the controls panel
    /// </summary>
    public bool AreControlsHidden { get; set; } = true;
}
