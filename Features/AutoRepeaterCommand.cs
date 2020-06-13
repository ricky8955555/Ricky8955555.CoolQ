using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;

namespace Ricky8955555.CoolQ.Features
{
    class AutoRepeaterCommand : Command
    {
        public override string ResponseCommand { get; } = "autorepeat";

        protected override string CommandUsage { get; } = "{0}autorepeat";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            if (Chattables.Switchs(e.Source))
                e.Source.Send("已开启自动复读 ヾ(^Д^*)/");
            else
                e.Source.Send("已关闭自动复读 ヾ(^Д^*)/");
        }
    }
}
