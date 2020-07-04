using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    internal static class Extensions
    {
        internal static void Switch<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
                list.Remove(item);
            else
                list.Add(item);
        }

        internal static bool Switchs<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
            {
                list.Remove(item);
                return false;
            }
            else
            {
                list.Add(item);
                return true;
            }
        }

        internal static bool Contains(this JArray jArray, object item, bool useExtension)
        {
            if (useExtension)
                return jArray.ToObject<List<object>>().Contains(item);
            else
                return jArray.Contains(item);
        }

        internal static JArray Remove(this JArray jArray, object item, bool useExtension)
        {
            if (useExtension)
            {
                var list = jArray.ToObject<List<object>>();
                list.Remove(item);
                return new JArray(list);
            }
            else
                throw new ArgumentException();
        }

        internal static void Add(this JObject jObject, JProperty content, bool canOverwrite)
        {
            if (canOverwrite || !jObject.ContainsKey(content.Name))
            {
                jObject.Remove(content.Name);
                jObject.Add(content);
            }
        }

        internal static string ToString(this IChattable chattable, bool useExtension)
        {
            if (chattable is IUser user && useExtension)
                return user.AsUser().ToString();
            else
                return chattable.ToString();
        }

        internal static bool ToBool(this string str, string str1) => str == str1;

        internal static bool? ToBool(this string str, string str1, string str2)
        {
            if (str == str1)
                return true;
            else if (str == str2)
                return false;
            else
                return null;
        }

        internal static bool Contains(this string str, string value, StringComparison comparisonType) => str.IndexOf(value, comparisonType) >= 0;

        internal static bool TryDeconstruct<T>(this ComplexMessage elements, out T element)
            where T : MessageElement
        {
            if (elements.Count == 0)
                element = null;
            else
            {
                element = elements[0] as T;
            }

            return element != null;
        }

        internal static bool TryDeconstruct<T1, T2>(this ComplexMessage elements, out T1 element1, out T2 element2)
            where T1 : MessageElement
            where T2 : MessageElement
        {
            if (elements.Count < 2)
            {
                element1 = null;
                element2 = null;
            }
            else
            {
                element1 = elements[0] as T1;
                element2 = elements[1] as T2;
            }

            return !(element1 == null && element2 == null);
        }
    }
}
