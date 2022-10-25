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

namespace Synapse.Demo.Application.Behaviors;

// TODO: Write tests
/// <summary>
/// A <see cref="IMiddleware<TRequest, TResult>"/> used to log a <see cref="TRequest"/>s
/// </summary>
/// <typeparam name="TRequest">The incoming <see cref="TRequest"/></typeparam>
/// <typeparam name="TResult">The outgoing <see cref="TResult"/></typeparam>
internal class RequestLogger<TRequest, TResult>
    : IMiddleware<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : IOperationResult
{
    /// <summary>
    /// Gets the <see cref="ILogger/>
    /// </summary>
    protected ILogger Logger { get; init; }

    /// <summary>
    /// Initializes a new <see cref="RequestLogger<TRequest, TResult>"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger/></param>
    public RequestLogger(ILogger<RequestLogger<TRequest, TResult>> logger)
    {
        this.Logger = logger;
    }

    /// <summary>
    /// Logs the request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResult> HandleAsync(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken = default)
    {
        var requestName = typeof(TRequest).Name;
        this.Logger.LogTrace($"Processing request '{requestName}': {request}");
        var reponse = (await next());
        this.Logger.LogTrace($"Processed request '{requestName}'");
        return reponse;
    }
}
