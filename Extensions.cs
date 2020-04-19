using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    static class Extensions
    {
        public static bool TryDeconstruct<T>(this ComplexMessage messageElements, out T t)
            where T : MessageElement
        {
            if (messageElements.Count < 1) {
                t = null;
            }
            else
            {
                t = messageElements[0] as T;
            }

            return !(t is null);
        }

        public static bool TryDeconstruct<T1, T2>(this ComplexMessage messageElements, out T1 t1, out T2 t2)
            where T1 : MessageElement
            where T2 : MessageElement
        {
            if (messageElements.Count < 2)
            {
                t1 = null;
                t2 = null;
            }
            else
            {
                t1 = messageElements[0] as T1;
                t2 = messageElements[1] as T2;
            }

            return !(t1 is null || t2 is null);
        }

        public static bool TryDeconstruct<T1, T2, T3>(this ComplexMessage messageElements, out T1 t1, out T2 t2, out T3 t3)
            where T1 : MessageElement
            where T2 : MessageElement
            where T3 : MessageElement
        {
            if (messageElements.Count < 3)
            {
                t1 = null;
                t2 = null;
                t3 = null;
            }
            else
            {
                t1 = messageElements[0] as T1;
                t2 = messageElements[1] as T2;
                t3 = messageElements[2] as T3;
            }

            return !(t1 is null || t2 is null || t3 is null);
        }
    }
}
