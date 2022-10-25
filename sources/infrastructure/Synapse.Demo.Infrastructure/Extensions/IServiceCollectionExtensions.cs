﻿// Copyright © 2022-Present The Synapse Authors. All rights reserved.
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

namespace Synapse.Demo.Infrastructure.Extensions.DependencyInjection;


/// <summary>
/// Extension methods for setting up the infrastructure services in an <see cref="IServiceCollection" />.
/// </summary>
public static class InfrastructureServiceCollectionExtensions
{
    /// <summary>
    /// Adds the infrastructure services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddDemoInfrastructure(this IServiceCollection services, IDemoApplicationOptions applicationOptions)
    {
        if (services == null) throw DomainException.ArgumentNull(nameof(services));
        services.AddSingleton<CloudEventFormatter, JsonEventFormatter>();
        services.AddCloudEventBus(builder =>
        {
            builder.WithBrokerUri(new (applicationOptions.CloudEventBroker));
        });
        return services;
    }
}
