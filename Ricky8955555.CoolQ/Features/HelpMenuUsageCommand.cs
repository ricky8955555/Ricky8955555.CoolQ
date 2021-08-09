using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Apps;

namespace Ricky8955555.CoolQ.Features
{
    public class HelpMenuUsageCommand : Command
    {
        public override string ResponseCommand { get; } = "help";

        protected override string CommandUsage { get; } = "{0}help (获取帮助菜单用法)";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            e.Reply("帮助菜单用法：\n" + AppBase.Apps.First(x => x.GetType() == typeof(HelpMenuApp)).Features.First(x => x.GetType() == typeof(HelpMenuCommand)).Usage);
        }
    }
}