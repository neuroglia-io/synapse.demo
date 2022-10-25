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

namespace Synapse.Demo.Integration.Models;

/// <summary>
/// Represents a location and its logical hierarchy
/// </summary>
public class Location
{
    /// <summary>
    /// The characters used to split the hierarchy
    /// </summary>
    public readonly static string LabelSeparator = ".";

    /// <summary>
    /// Gets the label that identifies the <see cref="Location"/>
    /// </summary>
    public string Label { get; init; } = null!;
    /// <summary>
    /// Gets the potential parent <see cref="Location"/>
    /// </summary>
    public Location? Parent { get; init; } = null;

    /// <summary>
    /// Returns the string representation of the <see cref="Location"/>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (Parent == null) return Label.ToString();
        return Parent.ToString() + LabelSeparator + Label.ToString();
    }
}
// TODO: review ToString and Integration vs Domain
