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
public interface IDemoApplicationOptions 
{
    /// <summary>
    /// Gets/Sets the sources of the <see cref="CloudEvent"/>s emitted by the application
    /// </summary>
    string CloudEventsSource { get; set; }

    /// <summary>
    /// Gets/Sets the schema registry holding the <see cref="CloudEvent"/>s data schemas
    /// </summary>
    string SchemaRegistry { get; set; }

    /// <summary>
    /// Gets/Sets the <see cref="CloudEvent"/>s broker
    /// </summary>
    string CloudEventBroker { get; set; }
}
