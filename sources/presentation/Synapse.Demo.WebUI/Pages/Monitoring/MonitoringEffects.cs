﻿namespace Synapse.Demo.WebUI.Pages.Monitoring.State;

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