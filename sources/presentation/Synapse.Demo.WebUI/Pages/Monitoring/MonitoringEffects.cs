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

[Effect]
public static class MonitoringEffects
{
    /// <summary>
    /// Handles the state initialization
    /// </summary>
    /// <param name="action">The <see cref="InitializeState"/> action</param>
    /// <param name="context">The <see cref="IEffectContext"/> context</param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static async Task On(InitializeState action, IEffectContext context)
    {
        try
        {
            var api = context.Services.GetRequiredService<IRestApiClient>();
            var devices = await api.GetDevices();
            context.Dispatcher.Dispatch(new ReplaceDevices(devices));
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Sends an <see cref="UpdateDeviceStateCommand"/> command
    /// </summary>
    /// <param name="action">The <see cref="InitializeState"/> action</param>
    /// <param name="context">The <see cref="IEffectContext"/> context</param>
    /// <returns></returns>
    public static async Task On(SendUpdateStateCommand action, IEffectContext context)
    {

        try
        {
            var api = context.Services.GetRequiredService<IRestApiClient>();
            var device = await api.UpdateDeviceState(action.Command);
            context.Dispatcher.Dispatch(new UpdateDeviceState(device.Id, device.State));
        }
        catch (Exception ex)
        {

        }
    }
}