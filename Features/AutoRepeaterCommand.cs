using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;
using static Ricky8955555.CoolQ.FeatureResources.AutoRepeaterResources;

namespace Ricky8955555.CoolQ.Features
{
    class AutoRepeaterCommand : Command
    {
        public override string ResponseCommand { get; } = "autorepeat";

        protected override string CommandUsage { get; } = "{0}autorepeat";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            if (Chattables.Switchs(e.Source))
                e.Source.Send(Started);
            else
                e.Source.Send(Stopped);
        }
    }
}
