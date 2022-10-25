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

namespace Synapse.Demo.Application.Commands.Devices;

// TODO: Write tests
/// <summary>
/// Represents the <see cref="IValidator"/> used to validate the <see cref="CreateDeviceCommand"/>
/// </summary>
internal class UpdateDeviceStateCommandValidator
    : AbstractValidator<UpdateDeviceStateCommand>
{
    /// <summary>
    /// Initializes a new <see cref="UpdateDeviceStateCommandValidator"/>
    /// </summary>
    public UpdateDeviceStateCommandValidator()
    {
        this.RuleFor(command => command.DeviceId)
            .NotEmpty();
    }
}
