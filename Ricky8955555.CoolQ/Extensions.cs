using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    static class Extensions
    {
        public static void Switch<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
                list.Remove(item);
            else
                list.Add(item);
        }

        public static bool Switchs<T>(this List<T> list, T item)
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

        public static bool Contains(this JArray jArray, object item, bool useExtension)
        {
            if (useExtension)
                return jArray.ToObject<List<object>>().Contains(item);
            else
                return jArray.Contains(item);
        }

        public static JArray Remove(this JArray jArray, object item, bool useExtension)
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

        public static void Add(this JObject jObject, JProperty content, bool canOverwrite)
        {
            if (canOverwrite || !jObject.ContainsKey(content.Name))
                jObject.Add(content);
        }

        public static void Operate(this JObject jObject, JProperty content, bool isAdding)
        {
            if (isAdding)
                jObject.Add(content);
            else
                jObject.Remove(content.Name);
        }

        public static void Operate(this JObject jObject, JProperty content, bool canOverwrite, bool isAdding)
        {
            if (isAdding)
                jObject.Add(content, canOverwrite);
            else
                jObject.Remove(content.Name);
        }

        public static string ToString(this IChattable chattable, bool useExtension)
        {
            if (chattable is IUser user && useExtension)
                return user.AsUser().ToString();
            else
                return chattable.ToString();
        }

        public static bool ToBool(this string str, string str1) => str == str1;

        public static bool? ToBool(this string str, string str1, string str2)
        {
            if (str == str1)
                return true;
            else if (str == str2)
                return false;
            else
                return null;
        }

        public static bool Contains(this string str, string value, StringComparison comparisonType) => str.IndexOf(value, comparisonType) >= 0;

        public static bool TryDeconstruct<T>(this ComplexMessage elements, out T element)
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

        public static bool TryDeconstruct<T1, T2>(this ComplexMessage elements, out T1 element1, out T2 element2)
            where T1 : MessageElement
            where T2 : MessageElement
        {
            if (elements.Count == 1)
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
