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

namespace Synapse.Demo.Common.Extensions;

/// <summary>
/// Extension methods for setting up the <see cref="JsonSerializerSettings"/>
/// </summary>
public static class JsonSerializerSettingsExtensions
{
    /// <summary>
    /// Configures a <see cref="JsonSerializerSettings"/>
    /// </summary>
    /// <param name="settings">The <see cref="JsonSerializerSettings"/> to configured</param>
    /// <returns>The configured <see cref="JsonSerializerSettings"/> so that additional calls can be chained.</returns>
    public static JsonSerializerSettings ConfigureSerializerSettings(this JsonSerializerSettings settings)
    {
        settings.ContractResolver = new NonPublicSetterContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
            {
                ProcessDictionaryKeys = false,
                OverrideSpecifiedNames = false,
                ProcessExtensionDataNames = false
            }
        };
        settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.DefaultValueHandling = DefaultValueHandling.Ignore;
        settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        settings.Converters.Add(new AbstractClassConverterFactory());
        return settings;
    }
}
