﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace Microsoft.Azure.Search.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Azure.Search.Models;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using Xunit;

    public class IndexFromTypeTests
    {
        [Fact]
        public void ReportsStringProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.String, fields["Text"].Type));
        }

        [Fact]
        public void ReportsInt32Properties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Int32, fields["Id"].Type));
        }

        [Fact]
        public void ReportsNullableInt32Properties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Int32, fields["NullableInt"].Type));
        }

        [Fact]
        public void ReportsInt64Properties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Int64, fields["BigNumber"].Type));
        }

        [Fact]
        public void ReportsDoubleProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Double, fields["Double"].Type));
        }

        [Fact]
        public void ReportsBooleanProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Boolean, fields["Flag"].Type));
        }

        [Fact]
        public void ReportsDateTimeOffsetProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.DateTimeOffset, fields["Time"].Type));
        }

        [Fact]
        public void ReportsDateTimeProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.DateTimeOffset, fields["TimeWithoutOffset"].Type));
        }

        [Fact]
        public void ReportsGeographyPointProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.GeographyPoint, fields["GeographyPoint"].Type));
        }

        [Fact]
        public void ReportsStringArrayProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Collection(DataType.String), fields["StringArray"].Type));
        }

        [Fact]
        public void ReportsIntArrayProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Collection(DataType.Int32), fields["IntArray"].Type));
        }

        [Fact]
        public void ReportsStringIListProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Collection(DataType.String), fields["StringIList"].Type));
        }

        [Fact]
        public void ReportsStringIEnumerableProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Collection(DataType.String), fields["StringIEnumerable"].Type));
        }

        [Fact]
        public void ReportsStringListProperties()
        {
            Run<ReflectableModel>(fields => Assert.Equal(DataType.Collection(DataType.String), fields["StringList"].Type));
        }

        private void OnlyTrueFor(Func<Field, bool> check, params string[] ids)
        {
            Run<ReflectableModel>(fields =>
            {
                foreach (var kv in fields)
                {
                    string id = kv.Key;
                    Field field = kv.Value;
                    bool result = check(field);
                    if (ids.Contains(id))
                    {
                        Assert.True(result);
                    }
                    else
                    {
                        Assert.False(result);
                    }
                }
            });
        }

        private void OnlyFalseFor(Func<Field, bool> check, params string[] ids) =>
            OnlyTrueFor(f => !check(f), ids);

        [Fact]
        public void ReportsKeyOnlyOnPropertyWithKeyAttribute()
        {
            OnlyTrueFor(field => field.IsKey, nameof(ReflectableModel.Id));

        }

        [Fact]
        public void ReportsIsSearchableOnlyOnPropertiesWithIsSearchableAttribute()
        {
            OnlyTrueFor(field => field.IsSearchable, nameof(ReflectableModel.Text), nameof(ReflectableModel.MoreText));
        }

        [Fact]
        public void IsFilterableOnlyOnPropertiesWithIsFilterableAttribute()
        {
            OnlyTrueFor(field => field.IsFilterable, nameof(ReflectableModel.FilterableText));
        }

        [Fact]
        public void IsSortableOnlyOnPropertiesWithIsSortableAttribute()
        {
            OnlyTrueFor(field => field.IsSortable, nameof(ReflectableModel.SortableText));
        }

        [Fact]
        public void IsFacetableOnlyOnPropertiesWithIsFacetableAttribute()
        {
            OnlyTrueFor(field => field.IsFacetable, nameof(ReflectableModel.FacetableText));
        }

        [Fact]
        public void IsRetrievableOnAllPropertiesExceptOnesWithIsNotRetrievableAttribute()
        {
            OnlyFalseFor(field => field.IsRetrievable, nameof(ReflectableModel.IrretrievableText));
        }

        [Fact]
        public void AnalyzerSetOnlyOnPropertiesWithAnalyzerAttribute()
        {
            OnlyTrueFor(field => field.Analyzer == AnalyzerName.EnMicrosoft, nameof(ReflectableModel.TextWithAnalyzer));
        }

        [Fact]
        public void SearchAnalyzerSetOnlyOnPropertiesWithSearchAnalyzerAttribute()
        {
            OnlyTrueFor(field => field.SearchAnalyzer == AnalyzerName.EsLucene, nameof(ReflectableModel.TextWithSearchAnalyzer));
        }

        [Fact]
        public void IndexAnalyzerSetOnlyOnPropertiesWithIndexAnalyzerAttribute()
        {
            OnlyTrueFor(field => field.IndexAnalyzer == AnalyzerName.Whitespace, nameof(ReflectableModel.TextWithIndexAnalyzer));
        }

        private void Run<T>(Action<Dictionary<string, Field>> run)
        {
            IList<Field> fields = FieldBuilder.BuildForType<T>(new ReadOnlyJsonContractResolver());
            Dictionary<string, Field> fieldMap = fields.ToDictionary(f => f.Name);
            run(fieldMap);
        }
    }
}
