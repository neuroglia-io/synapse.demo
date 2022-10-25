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
/// Represents the service used to handle <see cref="GenericListQuery{TEntity}"/> instances
/// </summary>
/// <typeparam name="TEntity">The type of entity to query</typeparam>
public class GenericListQueryHandler<TEntity>
    : QueryHandlerBase<TEntity>,
    IQueryHandler<GenericListQuery<TEntity>, IQueryable<TEntity>>
    where TEntity : class, IIdentifiable
{

    /// <inheritdoc/>
    public GenericListQueryHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IRepository<TEntity> repository)
        : base(loggerFactory, mediator, mapper, repository)
    {}

    /// <inheritdoc/>
    public virtual async Task<IOperationResult<IQueryable<TEntity>>> HandleAsync(GenericListQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(this.Ok(this.Repository.AsQueryable()));
    }

}