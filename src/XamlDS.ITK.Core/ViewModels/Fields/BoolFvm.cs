// <copyright file="BoolFvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents a boolean field ViewModel.
/// </summary>
/// <remarks>
/// Inherits <see cref="FieldVm{T}.Value"/>, <see cref="FieldVm{T}.IsHidden"/>, and <see cref="FieldVm{T}.IsReadOnly"/> from <see cref="FieldVm{T}"/> with <see cref="bool"/> as the value type.
/// </remarks>
public class BoolFvm(string name) : FieldVm<bool>(name)
{
}
