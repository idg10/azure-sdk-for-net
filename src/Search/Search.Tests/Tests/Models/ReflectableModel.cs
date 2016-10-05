// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace Microsoft.Azure.Search.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Azure.Search;

    public class ReflectableModel
    {
        [Key]
        public int Id { get; set; }
        public long BigNumber { get; set; }
        public double Double { get; set; }
        public bool Flag { get; set; }
        public DateTimeOffset Time { get; set; }
        public DateTime TimeWithoutOffset { get; set; }
        [IsSearchable]
        public string Text { get; set; }
        public string UnsearchableText { get; set; }
        [IsSearchable]
        public string MoreText { get; set; }
        public string[] StringArray { get; set; }
        public int[] IntArray { get; set; }
        public IList<string> StringIList { get; set; }
        public List<string> StringList { get; set; }
        public int? NullableInt { get; set; }
    }
}
