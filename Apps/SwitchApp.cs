using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class SwitchApp : App
    {
        public override string Name { get; } = "Switch";
        public override string DisplayName { get; } = "应用开关";
        public override string Usage { get; } = "{0}switch <应用名称> <on/off>";
        public override bool CanDisable { get; } = false;
        public override bool IsForAdministrator { get; } = true;

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string plainText = parameter?[0] as PlainText;
            string[] splitText = plainText?.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (splitText != null && splitText.Length == 2)
            {
                try
                {
                    var config = Configs.AppConfig;
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
