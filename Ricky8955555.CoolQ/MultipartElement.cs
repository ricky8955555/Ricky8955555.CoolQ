﻿using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    public class MultipartElement : MessageElement
    {
        public ComplexMessage Elements { get; }

        public override string ToSendableString() => Elements.ToSendableString();

        public MultipartElement(ComplexMessage elements) => Elements = elements;
    }
}