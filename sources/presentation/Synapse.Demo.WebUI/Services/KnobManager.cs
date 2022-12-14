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

namespace Synapse.Demo.WebUI;

/// <summary>
/// The service used to manage knobs
/// </summary>
public class KnobManager : IKnobManager
{
    /// <summary>
    /// Holds the <see cref="IJSRuntime"/>
    /// </summary>
    protected readonly IJSRuntime jsRuntime;
    /// <summary>
    /// Holds the <see cref="IJSInProcessObjectReference"/> for the knob module
    /// </summary>
    protected IJSInProcessObjectReference? knobModule = null;

    /// <summary>
    /// Initializes a new <see cref="KnobManager"/>
    /// </summary>
    /// <param name="jSRuntime">The <see cref="IJSRuntime"/></param>
    public KnobManager(IJSRuntime jSRuntime)
    {
        this.jsRuntime = jSRuntime;
    }

    /// <summary>
    /// Creates a knob with the provided <see cref="ElementReference"/>
    /// </summary>
    /// <param name="knobRef">The <see cref="ElementReference"/> to create the knob at</param>
    /// <param name="min">The minimum value of the knob</param>
    /// <param name="max">The maximum value of the knob</param>
    /// <param name="value">The current displayed value of the knob</param>
    /// <param name="icon">A material icon to display, if any</param>
    /// <returns></returns>
    public async Task<IJSObjectReference> CreateKnobAsync(ElementReference knobRef, int min, int max, int value, string? icon = null)
    {
        if (this.knobModule == null)
        {
            this.knobModule = await this.jsRuntime.InvokeAsync<IJSInProcessObjectReference>("import", "./js/knob.js");
        }
        return await this.knobModule.InvokeAsync<IJSObjectReference>("createKnob", knobRef, min, max, value, icon);
    }
}
