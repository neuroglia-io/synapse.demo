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
/// Represents the <see cref="IQuery"/> used to get an <see cref="IQueryable"/> of the entities of the specified type
/// </summary>
/// <typeparam name="TEntity">The type of <see cref="IEntity"/>The type of entities to query</typeparam>
public class GenericListQuery<TEntity>
    : Query<IQueryable<TEntity>>
    where TEntity : class, IIdentifiable
{}
