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

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Logging;
global using Neuroglia;
global using Neuroglia.Data;
global using Neuroglia.Data.EventSourcing;
global using Neuroglia.Data.EventSourcing.Services;
global using Neuroglia.Eventing;
global using System.Reactive;
global using System.Reactive.Linq;
global using System.Reactive.Subjects;
global using System.Reflection;

global using Synapse.Demo.Application.Configuration;
global using Synapse.Demo.Application.Extensions.DependencyInjection;
global using Synapse.Demo.Persistence.Read;
global using Synapse.Demo.Persistence.Write;