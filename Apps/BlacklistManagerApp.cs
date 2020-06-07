using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HuajiTech.CoolQ.CurrentPluginContext;

namespace Ricky8955555.CoolQ.Apps
{
    class BlacklistManagerApp : App
    {
        public override string Name { get; } = "BlacklistManager";
        public override string DisplayName { get; } = "黑名单管理器";
        public override string Usage { get; } = "{0}blacklist <add/remove> <QQ号/At>";
        public override bool CanDisable { get; } = false;

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string plainText = parameter?[0] as PlainText;
            string[] splitText = plainText?.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            var blacklistConfig = Configs.BlacklistConfig;
            var config = (JArray)blacklistConfig.Config;

            long number = long.MinValue;

            if (splitText != null && splitText.Length == 2)
                long.TryParse(splitText[1], out number);
            else if (splitText != null && splitText.Length == 1 && parameter?[1] is At at)
                number = at.TargetNumber;

            if (number > 0)
            {
                if (number == Configs.PluginConfig.Config["Administrator"].ToObject<long>() || number == Bot.CurrentUser.Number)
                    e.Source.Send(e.Sender.At() + "无法将管理员或机器人加入到黑名单 ─=≡Σ(((つ•̀ω•́)つ");
                else
                {
                    if (splitText[0] == "add")
                    {
                        if (config.Contains(number, true))
                            e.Source.Send($"{e.Sender.At()} {number} 已存在黑名单内 (ц｀ω´ц*)");
                        else
                        {
                            config.Add(number);
                            blacklistConfig.Save();
                            e.Source.Send($"{e.Sender.At()} 已将 {number} 加入黑名单 ❥(ゝω・✿ฺ)");
                        }
                    }
                    else if (splitText[0] == "remove")
                    {
                        if (config.Contains(number, true))
                        {
                            blacklistConfig.SetValueAndSave(config.Remove(number, true));
                            e.Source.Send($"{e.Sender.At()} 已将 {number} 移出黑名单 ❥(ゝω・✿ฺ)");
                        }
                        else
                            e.Source.Send($"{e.Sender.At()} {number} 不存在黑名单内 (ц｀ω´ц*)");
                    }
                    else
                        NotifyIncorrectUsage(e);
                }
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
