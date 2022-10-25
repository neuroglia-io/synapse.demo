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
/// Extension methods for setting up the web UI services in an <see cref="IServiceCollection" />.
/// </summary>
public static class WebUIServiceCollectionExtensions
{
    /// <summary>
    /// Adds the web UI services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="baseAddress">The web app base address</param>
    /// <returns>The configured <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWebUI(this IServiceCollection services, string baseAddress)
    {
        if (services == null) throw DomainException.ArgumentNull(nameof(services));
        services.AddWebUIClients(baseAddress)
            .AddWebUIMapper()
            .AddWebUIFlux()
            .AddWebUIServices();
        return services;
    }

    /// <summary>
    /// Adds the web UI rest & web socket client's configuration
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="baseAddress">The web app base address</param>
    /// <returns>The configured <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWebUIClients(this IServiceCollection services, string baseAddress)
    {
        var baseAddressUri = new Uri(baseAddress);
        services.AddScoped(sp => new HttpClient { BaseAddress = baseAddressUri });
        services.AddRestApiClient(http => http.BaseAddress = baseAddressUri);
        services.AddNewtonsoftJsonSerializer(options => options.ConfigureSerializerSettings());
        services.AddSingleton(provider =>
            new HubConnectionBuilder()
                .WithUrl($"{baseAddress}api/ws")
                .WithAutomaticReconnect()
                .AddNewtonsoftJsonProtocol(options => {
                    options.PayloadSerializerSettings = new JsonSerializerSettings().ConfigureSerializerSettings();
                })
                .Build()
        );
        return services;
    }

    /// <summary>
    /// Adds the web UI <see cref="Mapper"/> configuration
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The configured <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWebUIMapper(this IServiceCollection services)
    {
        Assembly integrationAssembly = typeof(Integration.ModelDto).Assembly;
        services.AddMapper(integrationAssembly);
        return services;
    }

    /// <summary>
    /// Adds the web UI flux configuration
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The configured <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWebUIFlux(this IServiceCollection services)
    {
        services.AddFlux(flux => flux.ScanMarkupTypeAssembly<App>());
        return services;
    }

    /// <summary>
    /// Adds the web UI services
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The configured <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddSingleton<IKnobManager, KnobManager>();
        return services;
    }
}
