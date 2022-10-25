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
/// Holds the state slices selectors for the <see cref="MonitoringState"/>
/// </summary>
public static class MonitoringSelectors
{
    /// <summary>
    /// Selects the list of <see cref="Device"/>s
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    public static IObservable<IEnumerable<Device>> SelectDevices(this IFeature<MonitoringState> feature)
    {
        return feature.Select(featureState => 
                featureState.Devices.Any() ? featureState.Devices.Values : new List<Device>().AsEnumerable()
            )
            .DistinctUntilChanged();
    }
    /// <summary>
    /// Selects the controls panel visibility state
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    public static IObservable<bool> SelectAreControlsHidden(this IFeature<MonitoringState> feature)
    {
        return feature.Select(featureState => featureState.AreControlsHidden)
            .DistinctUntilChanged();
    }
}
