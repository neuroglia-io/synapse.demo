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

[Reducer]
public static class MonitoringReducer
{
    /// <summary>
    /// Replaces the list of <see cref="Device"/>s
    /// </summary>
    /// <param name="state">The state to reduce</param>
    /// <param name="action">The action to reduce</param>
    /// <returns>The reduced state</returns>
    public static MonitoringState On(MonitoringState state, ReplaceDevices action)
    {
        return state with {
            Devices = action.Devices.ToDictionary(device => device.Id)
        };
    }

    /// <summary>
    /// Adds <see cref="Device"/> to the list
    /// </summary>
    /// <param name="state">The state to reduce</param>
    /// <param name="action">The action to reduce</param>
    /// <returns>The reduced state</returns>
    public static MonitoringState On(MonitoringState state, AddDevice action)
    {
        if (action?.Device == null) return state;
        var devices = new Dictionary<string, Device>(state.Devices);
        devices.Add(action.Device.Id, action.Device);
        return state with
        {
            Devices = devices
        };
    }

    /// <summary>
    /// Updates a <see cref="Device"/> state
    /// </summary>
    /// <param name="state">The state to reduce</param>
    /// <param name="action">The action to reduce</param>
    /// <returns>The reduced state</returns>
    public static MonitoringState On(MonitoringState state, UpdateDeviceState action)
    {
        if (action?.DeviceId == null) return state;
        if (!state.Devices.ContainsKey(action.DeviceId)) return state;
        var devices = new Dictionary<string, Device>(state.Devices);
        var device = devices[action.DeviceId];
        var newDevice = new Device()
        {
            Id = device.Id,
            Label = device.Label,
            Type = device.Type,
            Location = device.Location,
            State = action.State
        };
        devices.Remove(action.DeviceId);
        devices.Add(action.DeviceId, newDevice);
        return state with
        {
            Devices = devices
        };
    }

    /// <summary>
    /// Toggles the controls visibility state
    /// </summary>
    /// <param name="state">The state to reduce</param>
    /// <param name="action">The action to reduce</param>
    /// <returns></returns>
    public static MonitoringState On(MonitoringState state, ToggleControls action)
    {
        return state with
        {
            AreControlsHidden = !state.AreControlsHidden
        };
    }
}
