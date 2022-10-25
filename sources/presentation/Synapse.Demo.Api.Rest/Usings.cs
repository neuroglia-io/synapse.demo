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

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.OData;
global using Microsoft.AspNetCore.OData.NewtonsoftJson;
global using Microsoft.AspNetCore.OData.Query;
global using Microsoft.AspNetCore.OData.Query.Expressions;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.OData.Edm;
global using Microsoft.OData.UriParser;
global using Microsoft.OpenApi.Models;
global using Neuroglia;
global using Neuroglia.Data;
global using Neuroglia.Mapping;
global using Neuroglia.Mediation;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;
global using System.Linq.Expressions;
global using System.Net;
global using System.Reflection;

global using Synapse.Demo.Api.Rest.Services;
global using Synapse.Demo.Application.Configuration;
global using Synapse.Demo.Application.Extensions;
global using Synapse.Demo.Application.Extensions.DependencyInjection;
global using Synapse.Demo.Application.Services;
global using Synapse.Demo.Application.Queries;
global using Synapse.Demo.Common.Extensions;
global using Synapse.Demo.Integration.Commands.Devices;
global using Synapse.Demo.Integration.Models;