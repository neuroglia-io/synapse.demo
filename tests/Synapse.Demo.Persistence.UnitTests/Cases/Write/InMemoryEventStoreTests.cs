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

namespace Synapse.Demo.Persistence.UnitTests.Cases.Write;

public class InMemoryEventStoreTests
{
    [Fact]
    public async void AppendToStream_Should_Work()
    {
        var eventStore = EventStoreFactory.Create();
        var streamId = "test-stream";
        var domainEvent = DomainEventFactory.CreateDeviceCreatedDomainEvent();
        var events = new EventMetadata[]
        {
            new ("FakeType", domainEvent)
        };

        await eventStore.AppendToStreamAsync(streamId, events);
        var sourcedEvents = await eventStore.ReadAllEventsForwardAsync(streamId);

        sourcedEvents.Should().NotBeNull();
        sourcedEvents.Should().HaveCount(events.Length);
        sourcedEvents.First().Data.As<DeviceCreatedDomainEvent>().Should().BeEquivalentTo(events.First().Data);
    }

    [Fact]
    public async void AppendToStream_Wrong_Version_Should_Throw()
    {
        var eventStore = EventStoreFactory.Create();
        var streamId = "test-stream";
        var domainEvent = DomainEventFactory.CreateDeviceCreatedDomainEvent();
        var events = new EventMetadata[]
        {
            new ("FakeType", domainEvent)
        };

        await eventStore.AppendToStreamAsync(streamId, events);
        var act = () => eventStore.AppendToStreamAsync(streamId, events);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async void AppendToStream_Expected_Version_Should_Work()
    {
        var eventStore = EventStoreFactory.Create();
        var streamId = "test-stream";
        var domainEvent = DomainEventFactory.CreateDeviceCreatedDomainEvent();
        var events = new EventMetadata[]
        {
            new ("FakeType", domainEvent),
            new ("FakeType", domainEvent)
        };

        await eventStore.AppendToStreamAsync(streamId, events);
        await eventStore.AppendToStreamAsync(streamId, events, 2);
        var sourcedEvents = await eventStore.ReadAllEventsForwardAsync(streamId);

        sourcedEvents.Should().NotBeNull();
        sourcedEvents.Should().HaveCount(events.Length * 2);
        sourcedEvents.Last().Sequence.Should().Be(3);
        sourcedEvents.Last().Data.As<DeviceCreatedDomainEvent>().Should().BeEquivalentTo(events.Last().Data);
    }
}
