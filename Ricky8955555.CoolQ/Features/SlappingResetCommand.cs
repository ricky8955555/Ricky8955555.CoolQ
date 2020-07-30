using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Configuration;

namespace Ricky8955555.CoolQ.Features
{
    internal class SlappingResetCommand : Command
    {
        internal override string ResponseCommand { get; } = "slapping";

        protected override string CommandUsage { get; } = "{0}slapping (重置)";

        protected override void Invoking(MessageReceivedEventArgs e)
        {
            ((JObject)SlappingConfig.Config).Remove(e.Sender.Number.ToString());
            SlappingConfig.Save();

            e.Reply("你的拍一拍自定义语句重置好了 |ू･ω･` )");
        }
    }
}