using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Search.Tests.Tests.Models
{
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
