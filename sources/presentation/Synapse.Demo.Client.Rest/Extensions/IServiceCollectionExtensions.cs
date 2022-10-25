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

namespace Synapse.Demo.Client.Rest.Extensions;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>s
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures an HTTP client for REST API
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <param name="httpClientSetup">An <see cref="Action{T}"/> used to configure the <see cref="HttpClient"/> to use</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddRestApiClient(this IServiceCollection services, Action<HttpClient> httpClientSetup)
    {
        services.AddHttpClient(typeof(RestApiClient).Name, http => httpClientSetup(http));
        services.TryAddSingleton<IRestApiClient, RestApiClient>();
        return services;
    }
}
