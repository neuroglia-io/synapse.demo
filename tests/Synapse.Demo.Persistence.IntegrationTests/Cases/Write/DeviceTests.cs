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

using Synapse.Demo.Domain.Models;

namespace Synapse.Demo.Persistence.IntegrationTests.Cases.Write;

public class DeviceTests
{
    [Fact]
    public async void Repository_Should_Work()
    {
        var repository = await RepositoryFactory.Create<IRepository<Device, string>>();
        repository.Should().NotBeNull();
        await Task.CompletedTask;
    }

    [Fact]
    public async void Add_Should_Exist_Work()
    {
        var repository = await RepositoryFactory.Create<IRepository<Device, string>>();
        var device = DomainModelsFactory.CreateDevice();

        await repository.AddAsync(device);
        await repository.SaveChangesAsync();
        var writeModel = await repository.FindAsync(device.Id);

        writeModel.Should().NotBeNull();
        writeModel.Id.Should().Be(device.Id);
        writeModel.Label.Should().Be(device.Label);
        writeModel.Type.Should().Be(device.Type);
        writeModel.Location.Should().Be(device.Location);
        writeModel.State.Should().BeEquivalentTo(device.State);
    }
}
