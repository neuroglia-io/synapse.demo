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

global using Neuroglia;
global using Neuroglia.Data;

global using Synapse.Demo.Domain.Models;
global using Synapse.Demo.Domain.Events.Devices;
global using Synapse.Demo.Domain.Exceptions.Devices;
global using Synapse.Demo.Domain.Exceptions.Locations;
global using Synapse.Demo.Integration.Events.Devices;
// TODO: improve constructors by checking nullability of arguments