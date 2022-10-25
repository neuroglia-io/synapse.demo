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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Neuroglia.Eventing.Services;

namespace Synapse.Demo.Persistence.IntegrationTests.Data.Factories;

internal static class RepositoryFactory
{
    internal static async Task<T> Create<T>()
        where T : class, IRepository
    {
        var optionsDictionnary = new Dictionary<string, string>()
        {
            { "CloudEventsSource", "https://demo.synpase.com" },
            { "CloudEventBroker", "https://webhook.site/60a98df9-2b4b-47e5-a94e-f45b437424c6" },
            { "SchemaRegistry", "https://schema-registry.synapse.com" }
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(optionsDictionnary)
            .Build();
        ServiceCollection services = new();
        services.AddLogging();
        services.AddDemoApplication(configuration, demoBuilder =>
        {
            demoBuilder.AddInfrastructure();
            demoBuilder.AddPersistence();
        });
        var serviceProvider = services.BuildServiceProvider();
        var cloudEventBus = serviceProvider.GetRequiredService<CloudEventBus>();
        await cloudEventBus.StartAsync(new CancellationToken());
        await Task.Delay(1);
        await cloudEventBus.StopAsync(new CancellationToken());
        return serviceProvider.GetRequiredService<T>();
    }
}
