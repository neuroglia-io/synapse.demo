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

using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using Neuroglia.Eventing;
using Swashbuckle.AspNetCore.SwaggerUI;
using Synapse.Demo.Api.Rest.Extensions.DependencyInjection;
using Synapse.Demo.Api.WebSocket.Extensions.DependencyInjection;
using Synapse.Demo.Api.WebSocket.Hubs;
using Synapse.Demo.Application.Extensions.DependencyInjection;
using Synapse.Demo.Application.Services;
using Synapse.Demo.Infrastructure.Extensions.DependencyInjection;
using Synapse.Demo.Persistence.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logging =>
{
    logging.AddSimpleConsole(console =>
    {
        console.IncludeScopes = true;
        console.TimestampFormat = "[MM/dd/yyyy HH:mm:ss] ";
    });
});
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});
builder.Services.AddDemoApplication(builder.Configuration, demoBuilder =>
{
    demoBuilder.AddInfrastructure();
    demoBuilder.AddPersistence();
    demoBuilder.AddRestApi();
    demoBuilder.AddWebSocketApi();
});
builder.Services.AddHostedService<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCloudEvents();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseODataRouteDebug();
app.UseRouting();
app.UseSwagger(builder =>
{
    builder.RouteTemplate = "api/{documentName}/doc/oas.{json|yaml}";
});
app.UseSwaggerUI(builder =>
{
    builder.DocExpansion(DocExpansion.None);
    builder.SwaggerEndpoint("/api/v1/doc/oas.json", "Synapse API v1");
    builder.RoutePrefix = "api/doc";
});
app.MapHub<DemoApplicationHub>("/api/ws");
app.MapControllers();
app.MapFallbackToFile("index.html");

await app.RunAsync();