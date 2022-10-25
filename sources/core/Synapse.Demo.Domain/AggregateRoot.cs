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

namespace Synapse.Demo.Domain;


/// <summary>
/// Represents the base class of all the application's <see cref="IAggregateRoot"/> implementations
/// </summary>
public abstract class AggregateRoot
    : AggregateRoot<string>
{

    /// <summary>
    /// Initializes a new <see cref="AggregateRoot"/>
    /// </summary>
    /// <param name="id">The <see cref="AggregateRoot"/>'s unique identifier</param>
    protected AggregateRoot(string id)
        : base(id)
    {}

}