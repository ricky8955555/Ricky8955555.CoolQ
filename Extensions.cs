using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void Send(this ISendee sendee, IMessage message)
        {
            sendee.Send(message.ToString());
        }

        public static string ToString(this IChattable chattable, bool useExtension)
        {
            if (chattable is IUser user && useExtension)
                return user.AsUser().ToString();
            else
                return chattable.ToString();
        }
    }
}
