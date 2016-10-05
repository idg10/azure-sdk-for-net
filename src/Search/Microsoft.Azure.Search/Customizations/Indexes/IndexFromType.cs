﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace Microsoft.Azure.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Azure.Search.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class IndexFromType
    {
        /// <summary>
        /// Creates an <see cref="Index"/> with fields matching the properties of
        /// the type supplied.
        /// </summary>
        /// <typeparam name="T">
        /// The type for which fields will be created, based on its properties.
        /// </typeparam>
        /// <param name="contractResolver">
        /// Contract resolver that the <see cref="SearchIndexClient"/> will use.
        /// This ensures that the field names are generated in
        /// a way that is consistent with the way the model will be serialized.
        /// </param>
        /// <returns>An Index.</returns>
        public static Index Create<T>(IContractResolver contractResolver)
        {
            Index index = new Index();

            var contract = (JsonObjectContract) contractResolver.ResolveContract(typeof(T));
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
                    else if (attributeType == typeof(IsFilterableAttribute))
                    {
                        field.IsFilterable = true;
                    }
                    else if (attributeType == typeof(IsSortableAttribute))
                    {
                        field.IsSortable = true;
                    }
                    else if (attributeType == typeof(IsFacetableAttribute))
                    {
                        field.IsFacetable = true;
                    }
                    else if (attributeType == typeof(IsNotRetrievableAttribute))
                    {
                        field.IsRetrievable = false;
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
            if (propertyType == typeof(Microsoft.Spatial.GeographyPoint))
            {
                return DataType.GeographyPoint;
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
                nameof(propertyType));
        }

        private static Type GetElementTypeIfList(Type t) =>
            t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IList<>)
                ? t.GenericTypeArguments[0]
                : null;
    }
}
