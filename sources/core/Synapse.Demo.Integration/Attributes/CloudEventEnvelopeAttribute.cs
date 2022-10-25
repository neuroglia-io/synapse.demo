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

namespace Synapse.Demo.Integration.Attributes;

/// <summary>
/// Represents an <see cref="Attribute"/> used to configure the <see cref="CloudEvent"/> envelope of the marked <see cref="IntegrationEvent"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class CloudEventEnvelopeAttribute
    : Attribute
{

    /// <summary>
    /// Gets the domain aggregate type to build the <see cref="CloudEvent"/> for
    /// </summary>
    public virtual string AggregateType { get; set; }

    /// <summary>
    /// Gets the action name to build the <see cref="CloudEvent"/> for
    /// </summary>
    public virtual string ActionName { get; set; }

    /// <summary>
    /// Gets the <see cref="CloudEvent"/> data schema URI
    /// </summary>
    public virtual string? DataSchemaUri { get; set; }

    /// <summary>
    /// Initializes a new <see cref="CloudEventEnvelopeAttribute"/>
    /// </summary>
    /// <param name="aggregateType">The cloud event type</param>
    /// <param name="actionName">The cloud event type</param>
    /// <param name="dataSchemaUri">The cloud event data schema URI</param>
    /// <exception cref="DomainArgumentException"></exception>
    public CloudEventEnvelopeAttribute(string aggregateType, string actionName, string? dataSchemaUri = null)
    {
        if (string.IsNullOrWhiteSpace(aggregateType)) throw DomainException.ArgumentNullOrWhitespace(nameof(aggregateType));
        if (string.IsNullOrWhiteSpace(actionName)) throw DomainException.ArgumentNullOrWhitespace(nameof(actionName));
        // TODO: enforce formatting, "sluggify"
        this.AggregateType = aggregateType;
        this.ActionName = actionName;
        this.DataSchemaUri = dataSchemaUri;
    }
}
