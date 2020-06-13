using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Features
{
    class SwitchCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "switch";

        protected override string CommandUsage { get; } = "{0}switch <应用名称> <on/off>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            string[] splitText = plainText.ToString().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (splitText != null && splitText.Length == 2)
            {
                try
                {
                    var config = Common.AppConfig;
                    var app = Main.Apps.Where(x => x.Name == splitText[0]).Single();

                    if (app.CanDisable)
                    {
                        if (splitText[1] == "on")
                        {
                            config.Config[e.Source.ToString(true)][app.Name] = true;
                            config.Save();
                            e.Source.Send($"{e.Sender.At()} 已启用应用 {app.DisplayName}（{app.Name}） ✧(≖ ◡ ≖✿ ");
                        }
                        else if (splitText[1] == "off")
                        {
                            config.Config[e.Source.ToString(true)][app.Name] = false;
                            config.Save();
                            e.Source.Send($"{e.Sender.At()} 已停用应用 {app.DisplayName}（{app.Name}） ✧(≖ ◡ ≖✿ ");
                        }
                        else
                            NotifyIncorrectUsage(e);
                    }
                    else
                        e.Source.Send($"{e.Sender.At()} 该应用 {app.DisplayName}（{app.Name}）不允许被启用/停用 o(ﾟДﾟ)っ！");
                }
                catch (ArgumentException)
                {
                    NotifyIncorrectUsage(e);
                }
                catch (InvalidOperationException)
                {
                    NotifyIncorrectUsage(e);
                }
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
