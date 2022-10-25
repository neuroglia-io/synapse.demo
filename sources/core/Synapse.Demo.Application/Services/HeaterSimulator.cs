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

using Synapse.Demo.Application.Commands.Devices;

namespace Synapse.Demo.Application.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IHeater"/> interface
/// </summary>
public class HeaterSimulator
    : BackgroundService,
    IHeater
{

    /// <summary>
    /// Initializes a new <see cref="HeaterSimulator"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    /// <param name="logger">The service used to perform logging</param>
    public HeaterSimulator(IServiceProvider serviceProvider, ILogger<HeaterSimulator> logger)
    {
        this.ServiceProvider = serviceProvider;
        this.Logger = logger;
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; }

    /// <inheritdoc/>
    public bool IsPoweredOn { get; private set; }

    /// <summary>
    /// Gets the <see cref="HeaterSimulator"/>'s <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource CancellationTokenSource { get; private set; } = null!;

    /// <summary>
    /// Gets the <see cref="HeaterSimulator"/>'s heating <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource? HeatingCancellationTokenSource { get; private set; }

    /// <inheritdoc/>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task TurnOnAsync(CancellationToken cancellationToken = default)
    {
        if (!this.IsPoweredOn) _ = this.HeatAsync();
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task TurnOffAsync(CancellationToken cancellationToken = default)
    {
        if (!this.IsPoweredOn || this.HeatingCancellationTokenSource == null) return;
        this.HeatingCancellationTokenSource.Cancel();
        await Task.CompletedTask;
    }

    /// <summary>
    /// Heats the air
    /// </summary>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual async Task HeatAsync()
    {
        try
        {
            this.HeatingCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationTokenSource.Token);
            this.IsPoweredOn = true;
            using var scope = this.ServiceProvider.CreateScope();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var devices = scope.ServiceProvider.GetRequiredService<IRepository<Device, string>>();
            var device = await devices.FindAsync(ApplicationConstants.DeviceIds.Thermometer);
            this.Logger.LogInformation("Heater is now on");
            Thermometer thermometer = null!;
            if (device != null) thermometer = mapper.Map<Thermometer>(device);
            if (device == null || !thermometer.Temperature.HasValue)
            {
                await mediator.ExecuteAsync(new UpdateDeviceStateCommand(ApplicationConstants.DeviceIds.Heater, new { on = false }));
                return;
            }
            var temperature = thermometer.Temperature;
            var desiredTemperature = thermometer.DesiredTemperature;
            while (!this.HeatingCancellationTokenSource.IsCancellationRequested)
            {
                temperature++;
                var state = new
                {
                    temperature = temperature,
                    desired = desiredTemperature
                };
                await mediator.ExecuteAsync(new UpdateDeviceStateCommand(thermometer.Id, state));
                await Task.Delay(2000);
            }
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            this.Logger.LogError("An error occured while heating the air: {ex}", ex);
        }
        finally
        {
            this.IsPoweredOn = false;
            this.Logger.LogInformation("Heater is now off");
            this.HeatingCancellationTokenSource = null;
        }
    }

}