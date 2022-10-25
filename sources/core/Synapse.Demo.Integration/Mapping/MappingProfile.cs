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

using AutoMapper;

namespace Synapse.Demo.Integration.Mapping;

/// <summary>
/// Represents the Integration mapping <see cref="Profile"/>
/// </summary>
public class MappingProfile
    : Profile
{

    /// <summary>
    /// Gets a <see cref="HashSet{T}"/> containing the types of all existing <see cref="IMappingConfiguration"/>s
    /// </summary>
    protected HashSet<Type> KnownConfigurationTypes { get; init; }

    /// <summary>
    /// Gets the types of the <see cref="IMappingConfiguration"/>s
    /// </summary>
    protected IEnumerable<Type> MappingConfigurationTypes { get; init; }

    /// <summary>
    /// Initializes a new <see cref="MappingProfile"/>
    /// </summary>
    public MappingProfile()
    {
        this.AllowNullCollections = true;
        this.KnownConfigurationTypes = new HashSet<Type>();
        this.MappingConfigurationTypes = TypeCacheUtil.FindFilteredTypes("integration:mapping-configuration", t => !t.IsAbstract && !t.IsInterface && t.IsClass && typeof(IMappingConfiguration).IsAssignableFrom(t), this.GetType().Assembly);
        this.AddConfiguredMappings();
    }

    /// <summary>
    /// Configures the <see cref="MappingProfile"/> of classes marked with <see cref="IMappingConfiguration"/>
    /// </summary>
    protected void AddConfiguredMappings()
    {
        foreach (Type mappingConfigurationType in this.MappingConfigurationTypes)
        {
            this.ApplyConfiguration((IMappingConfiguration)Activator.CreateInstance(mappingConfigurationType, Array.Empty<object>())!);
            this.KnownConfigurationTypes.Add(mappingConfigurationType);
        }
    }
}
