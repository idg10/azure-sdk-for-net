using System;

namespace Microsoft.Azure.Search
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IsSearchableAttribute : Attribute
    {
    }
}
