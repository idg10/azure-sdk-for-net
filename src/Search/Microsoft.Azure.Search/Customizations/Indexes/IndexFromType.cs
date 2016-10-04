using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Azure.Search
{
    public static class IndexFromType
    {
        /// <summary>
        /// Creates an <see cref="Index"/> with fields matching the properties of
        /// the type supplied.
        /// </summary>
        /// <typeparam name="T">
        /// The type for which fields will be created, based on its properties.
        /// </typeparam>
        /// <param name="serializationSettings">
        /// Serialization settings to use. These should be the same as those in use by
        /// the search index client, to ensure that the field names are generated in
        /// a way that is consistent with the way the model will be serialized.
        /// </param>
        /// <returns>An Index.</returns>
        public static Index Create<T>(JsonSerializerSettings serializationSettings)
        {
            Index index = new Index();

            var contract = (JsonObjectContract) serializationSettings.ContractResolver.ResolveContract(typeof(T));
            var fields = new List<Field>();
            foreach (JsonProperty prop in contract.Properties)
            {
                DataType dataType = GetDataType(prop.PropertyType, prop.PropertyName);

                var field = new Field(prop.PropertyName, dataType);

                IList<Attribute> attributes = prop.AttributeProvider.GetAttributes(true);
                foreach (Attribute attribute in attributes)
                {
                    // Match on name to avoid dependency - don't want to force people not using
                    // this feature to bring in the annotations component.
                    Type attributeType = attribute.GetType();
                    if (attributeType.FullName == "System.ComponentModel.DataAnnotations.KeyAttribute")
                    {
                        field.IsKey = true;
                    }
                    else if (attributeType == typeof(IsSearchableAttribute))
                    {
                        field.IsSearchable = true;
                    }
                }

                fields.Add(field);
            }

            index.Fields = fields;
            return index;
        }

        private static DataType GetDataType(Type propertyType, string propertyName)
        {
            if (propertyType == typeof(string))
            {
                return DataType.String;
            }
            if (propertyType.IsConstructedGenericType &&
                propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return GetDataType(propertyType.GenericTypeArguments[0], propertyName);
            }
            if (propertyType == typeof(int))
            {
                return DataType.Int32;
            }
            if (propertyType == typeof(long))
            {
                return DataType.Int64;
            }
            if (propertyType == typeof(double))
            {
                return DataType.Double;
            }
            if (propertyType == typeof(bool))
            {
                return DataType.Boolean;
            }
            if (propertyType == typeof(DateTimeOffset) ||
                propertyType == typeof(DateTime))
            {
                return DataType.DateTimeOffset;
            }
            Type elementType = GetElementTypeIfList(propertyType);
            if (elementType != null)
            {
                return DataType.Collection(GetDataType(elementType, propertyName));
            }
            TypeInfo ti = propertyType.GetTypeInfo();
            var listElementTypes = ti
                .ImplementedInterfaces
                .Select(GetElementTypeIfList)
                .Where(p => p != null)
                .ToList();
            if (listElementTypes.Count == 1)
            {
                return DataType.Collection(GetDataType(listElementTypes[0], propertyName));
            }

            throw new ArgumentException(
                $"Property {propertyName} has unsupported type {propertyType}",
                    nameof(T));
        }

        private static Type GetElementTypeIfList(Type t) =>
            t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IList<>)
                ? t.GenericTypeArguments[0]
                : null;
    }
}
