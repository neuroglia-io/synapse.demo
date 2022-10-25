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

using Microsoft.AspNetCore.Mvc.Controllers;

namespace Synapse.Demo.Api.Rest.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up the <see cref="IDemoApplicationBuilder"/>
/// </summary>
public static class IDemoApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the DI pipeline for the RESTful API
    /// </summary>
    /// <param name="demoBuilder">the <see cref="IDemoApplicationBuilder"/> to configure</param>
    /// <returns></returns>
    public static IDemoApplicationBuilder AddRestApi(this IDemoApplicationBuilder demoBuilder)
    {
        if (demoBuilder == null) throw DomainException.ArgumentNull(nameof(demoBuilder));
        var searchBinder = new ODataSearchBinder();
        demoBuilder.Services.AddSingleton<ISearchBinder>(searchBinder);
        demoBuilder.Services.AddTransient<IODataQueryOptionsParser, ODataQueryOptionsParser>();
        demoBuilder.Services
                .AddControllers()
                .AddOData((options, provider) =>
                {
                    IEdmModelBuilder builder = provider.GetRequiredService<IEdmModelBuilder>();
                    options.AddRouteComponents("api/odata", builder.Build(), services => services.AddSingleton<ISearchBinder>(searchBinder))
                        .EnableQueryFeatures(50);
                    options.RouteOptions.EnableControllerNameCaseInsensitive = true;
                    options.RouteOptions.EnableActionNameCaseInsensitive = true;
                    options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
                })
                .AddODataNewtonsoftJson()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ConfigureSerializerSettings();
                })
                .AddApplicationPart(typeof(IDemoApplicationBuilderExtensions).Assembly)
                ;
        demoBuilder.Services.AddSwaggerGen(builder =>
        {
            builder.CustomOperationIds(o =>
            {
                if (!string.IsNullOrWhiteSpace(o.RelativePath)
                && o.RelativePath.Contains("odata"))
                    return o.RelativePath;
                return ((ControllerActionDescriptor)o.ActionDescriptor).ActionName;
            });
            builder.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            builder.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Demo API",
                Version = "v1",
                Description = "The Open API documentation for the Demo App API",
                Contact = new()
                {
                    Name = "Neuroglia SRL",
                    Email = "info@neuroglia.io",
                    Url = new Uri("https://www.neuroglia.io")
                }
            });
            builder.IncludeXmlComments(typeof(IDemoApplicationBuilderExtensions).Assembly.Location.Replace(".dll", ".xml"));
            builder.IncludeXmlComments(typeof(Integration.Models.Device).Assembly.Location.Replace(".dll", ".xml"));
        });
        return demoBuilder;
    }
}
