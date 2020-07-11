using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;

namespace Ricky8955555.CoolQ.Features
{
    internal class ConfigCommand : Command
    {
        protected override string CommandUsage { get; } = "{0}config";

        internal override string ResponseCommand { get; } = "config";

        protected override bool IsHandledAutomatically { get; } = false;

        internal static bool IsRunning = false;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            IsRunning = true;
        }
    }
}
