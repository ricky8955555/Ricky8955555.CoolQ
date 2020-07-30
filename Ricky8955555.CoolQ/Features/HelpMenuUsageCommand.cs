using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class HelpMenuUsageCommand : Command
    {
        internal override string ResponseCommand { get; } = "help";

        protected override string CommandUsage { get; } = "{0}help (获取帮助菜单用法)";

        protected override void Invoking(MessageReceivedEventArgs e)
        {
            e.Reply("帮助菜单用法：\n" + AppBase.Apps.Where(x => x.Name == "HelpMenu").Single().Features.Where(x => x.GetType().Name == "HelpMenuCommand").Single().Usage);
        }
    }
}
