﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace Microsoft.Azure.Search
{
    using System;
    using Microsoft.Azure.Search.Models;

    /// <summary>
    /// Indicates that the <see cref="Field"/> generated by <see cref="FieldBuilder"/> for
    /// the target property should have its <see cref="Field.IsFilterable"/> property set to true.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsFilterableAttribute : Attribute
    {
    }
}
