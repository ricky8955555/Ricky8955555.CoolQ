using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static Ricky8955555.CoolQ.Apps.ConfigApp;

namespace Ricky8955555.CoolQ.Features
{
    internal class ConfigCommand : Command
    {
        internal override string ResponseCommand { get; } = "config";

        protected override string CommandUsage { get; } = "{0}config";

        protected override bool IsHandledAutomatically { get; } = false;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            if (IsRunning)
            {
                e.Source.Send("配置正在运行中……");
                Handled = true;
            }
            else
                IsRunning = true;
        }
    }
}