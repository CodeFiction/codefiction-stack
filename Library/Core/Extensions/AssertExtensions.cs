﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeFiction.Stack.Library.Core.Extensions
{
    //This class was copied from ServiceStack library. (Servicestack.net)

    // TODO : Move to Utilities library
    public static class AssertExtensions
    {
        public static void ThrowOnFirstNull(params object[] objs)
        {
            foreach (object obj in objs)
            {
                ThrowIfNull(obj);
            }
        }

        public static void ThrowIfNull(this object obj)
        {
            ThrowIfNull(obj, null);
        }

        public static void ThrowIfNull(this object obj, string varName)
        {
            if (obj == null)
                throw new ArgumentNullException(varName ?? "object");
        }

        public static void ThrowIfNullOrEmpty(this string strValue)
        {
            ThrowIfNullOrEmpty(strValue, null);
        }

        public static void ThrowIfNullOrEmpty(this string strValue, string varName)
        {
            if (string.IsNullOrEmpty(strValue))
                throw new ArgumentNullException(varName ?? "string");
        }

        public static void ThrowIfNullOrEmpty(this ICollection collection)
        {
            ThrowIfNullOrEmpty(collection, null);
        }

        public static void ThrowIfNullOrEmpty(this ICollection collection, string varName)
        {
            string fieldName = varName ?? "collection";

            if (collection == null)
                throw new ArgumentNullException(fieldName);

            if (collection.Count == 0)
                throw new ArgumentException(fieldName + " is empty");
        }

        public static void ThrowIfNullOrEmpty<T>(this ICollection<T> collection)
        {
            ThrowIfNullOrEmpty(collection, null);
        }

        public static void ThrowIfNullOrEmpty<T>(this ICollection<T> collection, string varName)
        {
            string fieldName = varName ?? "collection";

            if (collection == null)
                throw new ArgumentNullException(fieldName);

            if (collection.Count == 0)
                throw new ArgumentException(fieldName + " is empty");
        }
    }
}