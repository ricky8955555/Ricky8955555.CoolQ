﻿using System;
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

            return t != null;
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

            return t1 != null && t2 != null;
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

            return t1 != null && t2 != null && t3 != null;
        }

        public static bool TryDeconstruct<T>(this ComplexMessage messageElements, out T t, out ComplexMessage complexMessage)
            where T : MessageElement
        {
            if (messageElements.Count < 2)
            {
                t = null;
                complexMessage = null;
            }
            else
            {
                t = messageElements[0] as T;
                complexMessage = new ComplexMessage(messageElements.Skip(1));
            }

            return t != null && complexMessage != null;
        }

        public static bool TryDeconstruct<T1, T2>(this ComplexMessage messageElements, out T1 t1, out T2 t2, out ComplexMessage complexMessage)
            where T1 : MessageElement
            where T2 : MessageElement
        {
            if (messageElements.Count < 3)
            {
                t1 = null;
                t2 = null;
                complexMessage = null;
            }
            else
            {
                t1 = messageElements[0] as T1;
                t2 = messageElements[1] as T2;
                complexMessage = new ComplexMessage(messageElements.Skip(2));
            }

            return t1 != null && t2 != null && complexMessage != null;
        }

        public static bool TryDeconstruct<T1, T2, T3>(this ComplexMessage messageElements, out T1 t1, out T2 t2, out T3 t3, out ComplexMessage complexMessage)
            where T1 : MessageElement
            where T2 : MessageElement
            where T3 : MessageElement
        {
            if (messageElements.Count < 4)
            {
                t1 = null;
                t2 = null;
                t3 = null;
                complexMessage = null;
            }
            else
            {
                t1 = messageElements[0] as T1;
                t2 = messageElements[1] as T2;
                t3 = messageElements[2] as T3;
                complexMessage = new ComplexMessage(messageElements.Skip(3));
            }

            return t1 != null && t2 != null && t3 != null && complexMessage != null;
        }
    }
}