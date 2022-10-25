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

namespace Synapse.Demo.WebUI.Extensions;

/// <summary>
/// Extension methods for <see cref="IIntegrationCommand"/>s
/// </summary>
public static class IIntegrationCommandExtensions
{
    /// <summary>
    /// Converts a <see cref="IIntegrationCommand"/> to a <see cref="CloudEventDto"/>
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static CloudEventDto? AsCloudEvent(this IIntegrationCommand command)
    {
        if (!command.GetType().TryGetCustomAttribute(out CloudEventEnvelopeAttribute cloudEventEnvelopeAttribute))
            return null;
        var eventIdentifier = $"{cloudEventEnvelopeAttribute.AggregateType}/{cloudEventEnvelopeAttribute.ActionName}/v1";
        return new(
            Guid.NewGuid().ToString(),
            "web-ui",
            $"{ApplicationConstants.CloudEventsType}/{eventIdentifier}",
            command
       );
    }
}
