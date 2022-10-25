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

namespace Synapse.Demo.Application.Queries;

/// <summary>
/// Represents the <see cref="IQuery"/> used to filter the entities of the specified type
/// </summary>
/// <typeparam name="TEntity">The type of <see cref="IEntity"/>The type of entities to query</typeparam>
public class GenericFilterQuery<TEntity>
    : Query<List<TEntity>>
    where TEntity : class, IIdentifiable
{

    /// <summary>
    /// Gets the <see cref="ODataQueryOptions"/> used to filter the entities
    /// </summary>
    public ODataQueryOptions<TEntity> Options { get; protected set; }

    /// <summary>
    /// Initializes a new <see cref="GenericFilterQuery{TEntity}"/>
    /// </summary>
    protected GenericFilterQuery()
    {
        this.Options = null!;
    }

    /// <summary>
    /// Initializes a new <see cref="GenericFilterQuery{TEntity}"/>
    /// </summary>
    /// <param name="options">The <see cref="ODataQueryOptions"/> used to filter the entities</param>
    public GenericFilterQuery(ODataQueryOptions<TEntity> options)
    {
        this.Options = options;
    }

}
