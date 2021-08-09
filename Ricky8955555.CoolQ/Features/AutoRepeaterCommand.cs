using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;

namespace Ricky8955555.CoolQ.Features
{
    public class AutoRepeaterCommand : Command
    {
        public override string ResponseCommand { get; } = "autorepeat";

        protected override string CommandUsage { get; } = "{0}autorepeat";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            if (Chattables.Switchs(e.Source))
                e.Source.Send("已开启自动复读 ヾ(^Д^*)/");
            else
                e.Source.Send("已关闭自动复读 ヾ(^Д^*)/");
        }
    }
}