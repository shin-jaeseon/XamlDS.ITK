// <copyright file="StringPvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Properties;

/// <summary>
/// Represents a string property ViewModel.
/// </summary>
/// <remarks>
/// Initializes the <see cref="PropertyEvm{T}.Value"/> to an empty string by default.
/// </remarks>
public class StringPvm : PropertyEvm<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringPvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public StringPvm(string name) : base(name)
    {
        Value = string.Empty;
    }
}
