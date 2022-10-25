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
/// Represents the base class for all <see cref="IDomainEvent"/>s in the Mozart context
/// </summary>
/// <typeparam name="TAggregate">The type of <see cref="IAggregateRoot"/> that has produced the <see cref="DomainEvent{TAggregate}"/></typeparam>
public abstract class DomainEvent<TAggregate>
    : DomainEvent<TAggregate, string>
    where TAggregate : class, IAggregateRoot<string>
{

    /// <summary>
    /// Initializes a new <see cref="DomainEvent{TAggregate}"/>
    /// </summary>
    protected DomainEvent()
    {}

    /// <summary>
    /// Initializes a new <see cref="DomainEvent{TAggregate}"/>
    /// </summary>
    /// <param name="aggregateId">The id of the <see cref="IAggregateRoot"/> to create the <see cref="DomainEvent{TAggregate}"/> for</param>
    protected DomainEvent(string aggregateId)
        : base(aggregateId)
    {}

}
