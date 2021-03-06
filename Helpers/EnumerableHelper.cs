﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Appercode
{
    /// <summary>
    /// Represents a set of helpers for IList interface
    /// </summary>
    internal static class EnumerableHelper
    {
        public static IEnumerable<T> AsReadonly<T>(this IEnumerable<T> source)
        {
            var collection = source as ICollection;
            if (collection == null)
            {
                return source.ToArray();
            }

            if (collection.Count == 0)
            {
                return Enumerable.Empty<T>();
            }

            var result = new T[collection.Count];
            collection.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Enumerates a list in specified range
        /// </summary>
        public static IEnumerable<T> EnumerableRange<T>(this IList<T> list, int start, int count, bool reverse = false)
        {
            if (reverse)
            {
                int end = start + count - 1;

                for (int i = end; i >= start; --i)
                {
                    yield return list[i];
                }
            }
            else
            {
                int end = start + count;

                for (int i = start; i < end; ++i)
                {
                    yield return list[i];
                }
            }
        }

        /// <summary>
        /// Extracts a list in specified range
        /// </summary>
        public static IList<T> ListRange<T>(this IList<T> value, int start, int count, bool reverse = false)
        {
            if (count == 0)
            {
                return new T[0];
            }

            var result = new T[count];
            var list = value as List<T>;
            if (list != null)
            {
                list.CopyTo(start, result, 0, count);
            }
            else
            {
                var array = value as T[];
                if (array != null)
                {
                    Array.Copy(array, start, result, 0, count);
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = value[i + start];
                    }
                }
            }

            if (reverse)
            {
                Array.Reverse(result);
            }

            return result;
        }
    }
}
