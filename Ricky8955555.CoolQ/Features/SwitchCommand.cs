using System;
using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class SwitchCommand : Command<PlainText, PlainText>
    {
        public override string ResponseCommand { get; } = "switch";

        protected override string CommandUsage { get; } = "{0}switch <应用名称> <on/off>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText appName, PlainText operationText, ComplexMessage elements)
        {
            try
            {
                var config = Configurations.AppStatusConfig;
                var app = AppUtilities.GetApps(e.Source, e.Subject).Where(x => x.Name.ToLower() == appName.Content.ToLower()).Single();
                bool? operation = operationText.Content.ToLower().ToBool("on", "off");

                if (app.CanDisable)
                {
                    if (operation.HasValue)
                    {
                        config.Config[e.Source.ToUniversalString()][app.Name] = operation.Value;
                        e.Reply($"已{(operation.Value ? "启用" : "停用")}应用 {app.DisplayName}（{app.Name}） ✧(≖ ◡ ≖✿ ");
                        config.Save();
                    }
                }
                else
                    e.Reply($"该应用 {app.DisplayName}（{app.Name}）不允许被启用/停用 o(ﾟДﾟ)っ！");
            }
            catch (InvalidOperationException)
            {
                e.Reply($"应用 {appName} 不存在 (•́へ•́╬)");
            }
        }
    }
}