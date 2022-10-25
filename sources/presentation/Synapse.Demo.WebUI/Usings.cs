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

global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using Microsoft.AspNetCore.SignalR.Client;
global using Microsoft.JSInterop;
global using Neuroglia;
global using Neuroglia.Data;
global using Neuroglia.Data.Flux;
global using Neuroglia.Mapping;
global using Neuroglia.Serialization;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;
global using System.Reactive.Linq;
global using System.Reflection;

global using Synapse.Demo.Client.Rest.Extensions;
global using Synapse.Demo.Client.Rest.Services;
global using Synapse.Demo.Common.Extensions;
global using Synapse.Demo.Integration.Attributes;
global using Synapse.Demo.Integration.Commands;
global using Synapse.Demo.Integration.Models;
global using Synapse.Demo.WebUI;
global using Synapse.Demo.WebUI.Extensions;
global using Synapse.Demo.Common;
