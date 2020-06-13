using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    abstract class Feature
    {
        public virtual string Usage { get; } = null;

        public abstract void Invoke(MessageReceivedEventArgs e);

        public void NotifyIncorrectUsage(MessageReceivedEventArgs e)
        {
            e.Source.Send($"{e.Sender.At()} 参数错误 (￣３￣)a ，具体用法：{Usage}");
        }
    }
}
