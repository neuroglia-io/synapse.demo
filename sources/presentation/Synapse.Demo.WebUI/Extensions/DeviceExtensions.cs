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

using System.Text.RegularExpressions;

namespace Synapse.Demo.WebUI.Extensions;

/// <summary>
/// Extension methods for <see cref="Device"/>s
/// </summary>
public static class DeviceExtensions
{
    /// <summary>
    /// Returns a <see cref="DeviceWidgetViewModel"/> for the targeted <see cref="Device"/>
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public static DeviceWidgetViewModel? AsViewModel(this Device? device, IMapper mapper)
    {
        if (device == null) return null;
        DeviceWidgetViewModel viewModel = new DeviceWidgetViewModel()
        {
            Id = device.Id,
            Label = device.Label
        };
        switch (device.Type)
        {
            case ApplicationConstants.DeviceTypes.ThermometerSensor:
                {
                    var thermometer = mapper.Map<Thermometer>(device);
                    viewModel.IsActive = true;
                    var displayedTemperature = thermometer.DisplayedTemperature;
                    if (thermometer.DesiredTemperature != null && thermometer.DesiredTemperature != thermometer.Temperature)
                    {
                        displayedTemperature += "->" + thermometer.DisplayedDesiredTemperature;
                    }
                    viewModel.Data = displayedTemperature;
                    if (thermometer.Temperature.HasValue)
                    {
                        viewModel.Hero = new KnobHeroViewModel(0, 50, thermometer.Temperature.Value, "thermometer");
                    }
                    break;
                }
            case ApplicationConstants.DeviceTypes.HydrometerSensor:
                {
                    var hydrometer = mapper.Map<Hydrometer>(device);
                    viewModel.IsActive = true;
                    viewModel.Data = hydrometer.DisplayedHumidity;
                    if (hydrometer.Humidity.HasValue)
                    {
                        viewModel.Hero = new KnobHeroViewModel(0, 100, hydrometer.Humidity.Value, "humidity_low");
                    }
                    break;
                }
            case ApplicationConstants.DeviceTypes.MotionSensor:
                {
                    var switchable = mapper.Map<Switchable>(device);
                    if (switchable.IsTurnedOn)
                    {
                        viewModel.Hero = "motion_sensor_active";
                        viewModel.Data = "-ON-";
                        viewModel.IsActive = true;
                    }
                    else
                    {
                        viewModel.Hero = "motion_sensor_idle";
                        viewModel.Data = "-OFF-";
                        viewModel.IsActive = false;
                    }
                    break;
                }
            case ApplicationConstants.DeviceTypes.LightsSwitch:
                {
                    viewModel.Hero = "light";
                    var switchable = mapper.Map<Switchable>(device);
                    if (switchable.IsTurnedOn)
                    { 
                        viewModel.Data = "-ON-";
                        viewModel.IsActive = true;
                    }
                    else
                    {
                        viewModel.Data = "-OFF-";
                        viewModel.IsActive = false;
                    }
                    break;
                }
            case ApplicationConstants.DeviceTypes.HeaterEquipment:
                {
                    viewModel.Hero = "fireplace";
                    var switchable = mapper.Map<Switchable>(device);
                    if (switchable.IsTurnedOn)
                    {
                        viewModel.Data = "-ON-";
                        viewModel.IsActive = true;
                    }
                    else
                    {
                        viewModel.Data = "-OFF-";
                        viewModel.IsActive = false;
                    }
                    break;
                }
            case ApplicationConstants.DeviceTypes.AirConditioningEquipment:
                {
                    var switchable = mapper.Map<Switchable>(device);
                    if (switchable.IsTurnedOn)
                    {
                        viewModel.Hero = "mode_cool";
                        viewModel.Data = "-ON-";
                        viewModel.IsActive = true;
                    }
                    else
                    {
                        viewModel.Hero = "mode_cool_off";
                        viewModel.Data = "-OFF-";
                        viewModel.IsActive = false;
                    }
                    break;
                }
            case ApplicationConstants.DeviceTypes.BlindsEquipment:
                {
                    var switchable = mapper.Map<Switchable>(device);
                    if (switchable.IsTurnedOn)
                    {
                        viewModel.Hero = "blinds";
                        viewModel.Data = "-OPEN-";
                        viewModel.IsActive = true;
                    }
                    else
                    {
                        viewModel.Hero = "blinds_closed";
                        viewModel.Data = "-CLOSED-";
                        viewModel.IsActive = false;
                    }
                    break;
                }
        }
        return viewModel;
    }
}
