// Copyright (c) Microsoft Corporation. All rights reserved.
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
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.String, fields["Text"].Type));
        }

        [Fact]
        public void ReportsInt32Properties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Int32, fields["Id"].Type));
        }

        [Fact]
        public void ReportsNullableInt32Properties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Int32, fields["NullableInt"].Type));
        }

        [Fact]
        public void ReportsInt64Properties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Int64, fields["BigNumber"].Type));
        }

        [Fact]
        public void ReportsDoubleProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Double, fields["Double"].Type));
        }

        [Fact]
        public void ReportsBooleanProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Boolean, fields["Flag"].Type));
        }

        [Fact]
        public void ReportsDateTimeOffsetProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.DateTimeOffset, fields["Time"].Type));
        }

        [Fact]
        public void ReportsDateTimeProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.DateTimeOffset, fields["TimeWithoutOffset"].Type));
        }

        [Fact]
        public void ReportsStringArrayProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Collection(DataType.String), fields["StringArray"].Type));
        }

        [Fact]
        public void ReportsIntArrayProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Collection(DataType.Int32), fields["IntArray"].Type));
        }

        [Fact]
        public void ReportsStringIListProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Collection(DataType.String), fields["StringIList"].Type));
        }

        [Fact]
        public void ReportsStringListProperties()
        {
            Run<ReflectableModel>((index, fields) => Assert.Equal(DataType.Collection(DataType.String), fields["StringList"].Type));
        }

        [Fact]
        public void ReportsKeyOnPropertyWithKeyAttribute()
        {
            Run<ReflectableModel>((index, fields) => Assert.True(fields["Id"].IsKey));

        }

        [Fact]
        public void DoesNotReportKeyOnPropertiesWithoutKeyAttribute()
        {
            Run<ReflectableModel>((index, fields) =>
                Assert.False(fields.Any(f => f.Key != "Id" && f.Value.IsKey)));

        }

        [Fact]
        public void ReportsIsSearchableOnPropertyWithIsSearchableAttribute()
        {
            Run<ReflectableModel>((index, fields) =>
            {
                Assert.True(fields["Text"].IsSearchable);
                Assert.True(fields["MoreText"].IsSearchable);
            });

        }

        [Fact]
        public void DoesNotReportIsSearchableOnPropertiesIsSearchableKeyAttribute()
        {
            Run<ReflectableModel>((index, fields) =>
                Assert.False(fields.Any(f => f.Key != "Text" && f.Key != "MoreText" && f.Value.IsSearchable)));
        }

        private void Run<T>(Action<Index, Dictionary<string, Field>> run)
        {
            Index index = IndexFromType.Create<T>(
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    ContractResolver = new ReadOnlyJsonContractResolver(),
                    Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
                });
            Dictionary<string, Field> fields = index.Fields.ToDictionary(f => f.Name);
            run(index, fields);
        }
    }
}
