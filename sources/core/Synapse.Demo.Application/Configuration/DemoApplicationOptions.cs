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

namespace Synapse.Demo.Application.Configuration;

/// <summary>
/// Represents the application configuration options
/// </summary>
public class DemoApplicationOptions
    : IDemoApplicationOptions
{
    /// <inheritdoc/>
    public string CloudEventsSource { get; set; } = String.Empty;

    /// <inheritdoc/>
    public string SchemaRegistry { get; set; } = null!;

    /// <inheritdoc/>
    public string CloudEventBroker { get; set; } = null!;

    /// <summary>
    /// Initialises a new <see cref="DemoApplicationOptions"/>
    /// </summary>
    public DemoApplicationOptions()
    { }
}
