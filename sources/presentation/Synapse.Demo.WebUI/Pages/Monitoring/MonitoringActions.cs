﻿using Synapse.Demo.Integration.Commands.Devices;

namespace Synapse.Demo.WebUI.Pages.Monitoring.State;

/// <summary>
/// Triggers the state initialization
/// </summary>
public class InitializeState { }

/// <summary>
/// Adds a <see cref="Device"/> to the list of monitored devices
/// </summary>
public class AddDevice
{
    /// <summary>
    /// Get the <see cref="Device"/> to monitor
    /// </summary>
    public Device Device { get; }

    /// <summary>
    /// Initializes a new <see cref="AddDevice"/> action
    /// </summary>
    /// <param name="device">The <see cref="Device"/> to monitor</param>
    public AddDevice(Device device)
    {
        Device = device;
    }
}

/// <summary>
/// Replaces the list of monitored <see cref="Device"/>s
/// </summary>
public class ReplaceDevices
{
    /// <summary>
    /// Get the list <see cref="Device"/>s to monitor
    /// </summary>
    public IEnumerable<Device> Devices { get; }

    /// <summary>
    /// Initializes a new <see cref="ReplaceDevices"/> action
    /// </summary>
    /// <param name="devices">The <see cref="Device"/>s to monitor</param>
    public ReplaceDevices(IEnumerable<Device> devices)
    {
        this.Devices = devices ?? new List<Device>();
    }
}

/// <summary>
/// Updates a <see cref="Device"/> state
/// </summary>
public class UpdateDeviceState
{
    /// <summary>
    /// Get the <see cref="Device"/> id to update the state of
    /// </summary>
    public string DeviceId { get; }

    /// <summary>
    /// Get the updated state
    /// </summary>
    public Object? State { get; }

    /// <summary>
    /// Initiliazes a new <see cref="UpdateDeviceState"/> action
    /// </summary>
    /// <param name="deviceId">The <see cref="Device"/> id to update the state of</param>
    /// <param name="state">The updated state</param>
    public UpdateDeviceState(string deviceId, object? state)
    {
        this.DeviceId = deviceId;
        this.State = state;
    }
}

/// <summary>
/// Toggles the controls panel state
/// </summary>
public class ToggleControls { }

/// <summary>
/// Send a command to update a device state
/// </summary>
public class SendUpdateStateCommand
{
    /// <summary>
    /// Gets the command to send
    /// </summary>
    public UpdateDeviceStateCommand Command { get; }

    /// <summary>
    /// Initializes a new <see cref="SendUpdateStateCommand"/>
    /// </summary>
    /// <param name="command">The command to send</param>
    public SendUpdateStateCommand(UpdateDeviceStateCommand command)
    {
        this.Command = command;
    }
}