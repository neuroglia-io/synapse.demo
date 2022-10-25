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

using Synapse.Demo.Domain.Models;

namespace Synapse.Demo.Application.UnitTests.Data.Factories;

internal static class DeviceFactory
{
    public static Domain.Models.Device CreateDomainDevice()
    {
        return new Domain.Models.Device("device-123", "my device", "lamp", $"indoors{Domain.Models.Location.LabelSeparator}kitchen", new { Hello = "World" });
    }

    public static Integration.Models.Device CreatePseudoThermometer()
    {
        return new Integration.Models.Device()
        {
            Id = "thermometer",
            Label = "Temperature",
            Type = "sensor.thermometer",
            Location = new Integration.Models.Location() { Label = "indoor" }, 
            State = new { temperature = 17, desired = 19 }
        };
    }
}
