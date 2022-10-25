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
/// Represents the service used to handle <see cref="GenericFilterQuery{TEntity}"/> instances
/// </summary>
/// <typeparam name="TEntity">The type of entity to filter</typeparam>
public class GenericFilterQueryHandler<TEntity>
    : QueryHandlerBase<TEntity>,
    IQueryHandler<GenericFilterQuery<TEntity>, List<TEntity>>
    where TEntity : class, IIdentifiable
{

    /// <summary>
    /// Gets the service used to bind ODATA searches
    /// </summary>
    protected ISearchBinder SearchBinder { get; }

    /// <summary>
    /// Gets the current <see cref="IEdmModel"/>
    /// </summary>
    protected IEdmModel EdmModel { get; }

    /// <inheritdoc/>
    public GenericFilterQueryHandler(ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper, IRepository<TEntity> repository, ISearchBinder searchBinder, IEdmModel edmModel)
        : base(loggerFactory, mediator, mapper, repository)
    {
        this.SearchBinder = searchBinder;
        this.EdmModel = edmModel;
    }

    /// <inheritdoc/>
    public virtual async Task<IOperationResult<List<TEntity>>> HandleAsync(GenericFilterQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        var toFilter = (await this.Repository.ToListAsync(cancellationToken)).AsQueryable();
        if (query.Options?.Search != null)
        {
            var searchExpression = (Expression<Func<TEntity, bool>>)this.SearchBinder.BindSearch(query.Options.Search.SearchClause, new(this.EdmModel, new(), typeof(TEntity)));
            toFilter = toFilter.Where(searchExpression);
        }
        var filtered = query.Options?.ApplyTo(toFilter);
        if (filtered == null)
            filtered = toFilter;
        return this.Ok(filtered.OfType<TEntity>().ToList());
    }

}