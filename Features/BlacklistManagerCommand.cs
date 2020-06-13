using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HuajiTech.CoolQ.CurrentPluginContext;
using static Ricky8955555.CoolQ.Common;

namespace Ricky8955555.CoolQ.Features
{
    class BlacklistManagerCommand : Command<PlainText, At>
    {
        public override string ResponseCommand { get; } = "blacklist";

        protected override string CommandUsage { get; } = "{0}blacklist <add/remove> <At>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, At at)
        {
            string operating = plainText.ToString().Trim();
            var config = (JArray)BlacklistConfig.Config;
            long number = at.TargetNumber;

            if (number == Administrator || number == Bot.CurrentUser.Number)
                e.Source.Send(e.Sender.At() + "无法将管理员或机器人加入到黑名单 ─=≡Σ(((つ•̀ω•́)つ");
            else
            {
                if (operating == "add")
                {
                    if (config.Contains(number, true))
                        e.Source.Send($"{e.Sender.At()} {number} 已存在黑名单内 (ц｀ω´ц*)");
                    else
                    {
                        config.Add(number);
                        BlacklistConfig.Save();
                        e.Source.Send($"{e.Sender.At()} 已将 {number} 加入黑名单 ❥(ゝω・✿ฺ)");
                    }
                }
                else if (operating == "remove")
                {
                    if (config.Contains(number, true))
                    {
                        BlacklistConfig.SetValueAndSave(config.Remove(number, true));
                        e.Source.Send($"{e.Sender.At()} 已将 {number} 移出黑名单 ❥(ゝω・✿ฺ)");
                    }
                    else
                        e.Source.Send($"{e.Sender.At()} {number} 不存在黑名单内 (ц｀ω´ц*)");
                }
                else
                    NotifyIncorrectUsage(e);
            }
        }
    }
}
