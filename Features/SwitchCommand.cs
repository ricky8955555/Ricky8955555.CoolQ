using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;
using static Ricky8955555.CoolQ.FeatureResources.SwitchResources;
using static Ricky8955555.CoolQ.Utilities;

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
                    var config = Commons.AppConfig;
                    var app = GetApps(e.Source).Where(x => x.Name == splitText[0]).Single();

                    if (app.CanDisable)
                    {
                        if (splitText[1] == "on")
                        {
                            config.Config[e.Source.ToString(true)][app.Name] = true;
                            config.Save();
                            e.Reply(string.Format(Enabled, app.DisplayName, app.Name));
                        }
                        else if (splitText[1] == "off")
                        {
                            config.Config[e.Source.ToString(true)][app.Name] = false;
                            config.Save();
                            e.Reply(string.Format(Disabled, app.DisplayName, app.Name));
                        }
                        else
                            NotifyIncorrectUsage(e);
                    }
                    else
                        e.Reply(string.Format(NotAllowed, app.DisplayName, app.Name));
                }
                catch (ArgumentException)
                {
                    e.Source.Send(NotExist);
                }
                catch (InvalidOperationException)
                {
                    e.Source.Send(NotExist);
                }
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
