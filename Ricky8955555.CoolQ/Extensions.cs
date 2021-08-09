using System;
using System.Collections.Generic;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    public static class Extensions
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

        public static string ToUniversalString(this IChattable chattable)
        {
            return chattable is IUser user ? user.AsUser().ToString() : chattable.ToString();
        }

        public static bool? ToBool(this string str, string trueString, string falseString)
        {
            if (str == trueString)
                return true;
            else if (str == falseString)
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
                element = elements[0] as T;

            return element is not null;
        }

        public static bool TryDeconstruct<T1, T2>(this ComplexMessage elements, out T1 element1, out T2 element2)
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

            return !(element1 is null || element2 is null);
        }

        public static bool TryDeconstruct<T1, T2, T3>(this ComplexMessage elements, out T1 element1, out T2 element2, out T3 element3)
            where T1 : MessageElement
            where T2 : MessageElement
            where T3 : MessageElement
        {
            if (elements.Count < 3)
            {
                element1 = null;
                element2 = null;
                element3 = null;
            }
            else
            {
                element1 = elements[0] as T1;
                element2 = elements[1] as T2;
                element3 = elements[2] as T3;
            }

            return !(element1 is null || element2 is null || element3 is null);
        }

        public static ComplexMessage Flatten(this ComplexMessage elements)
        {
            var newElements = new ComplexMessage();

            foreach (var element in elements)
            {
                if (element is MultipartElement multipartElement)
                    newElements.Add(multipartElement.Elements);
                else
                    newElements.Add(element);
            }

            return newElements;
        }

        public static MessageElement ToMessageElement(this ComplexMessage elements)
        {
            if (elements.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(elements));
            else if (elements.Count == 1)
                return elements[0];
            else
                return new MultipartElement(elements);
        }
    }
}