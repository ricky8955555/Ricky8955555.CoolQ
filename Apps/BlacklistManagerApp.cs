using HuajiTech.CoolQ;
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
    class BlacklistManagerApp : App
    {
        public override string Name { get; } = "BlacklistManager";
        public override string DisplayName { get; } = "黑名单管理器";
        public override string Usage { get; } = "{0}blacklist <add/remove> <QQ号/At>";

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string plainText = parameter?[0] as PlainText;
            string[] splitText = plainText?.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            var blacklistConfig = Configs.BlacklistConfig;
            var config = (JArray)blacklistConfig.Config;


            var list = config.ToObject<List<long>>();
            long number = long.MinValue;

            if (splitText != null && splitText.Length == 2)
                long.TryParse(splitText[1], out number);
            else if (splitText != null && splitText.Length == 1 && parameter?[1] is At at)
                number = at.TargetNumber;

            if (number > 0)
            {
                if (number == Configs.PluginConfig.Config["Administrator"].ToObject<long>())
                    e.Source.Send(e.Sender.At() + "无法将管理员加入到黑名单 ─=≡Σ(((つ•̀ω•́)つ");
                else
                {
                    if (splitText[0] == "add")
                    {
                        if (list.Contains(number))
                            e.Source.Send($"{e.Sender.At()} {number} 已存在黑名单内 (ц｀ω´ц*)");
                        else
                        {
                            config.Add(number);
                            e.Source.Send($"{e.Sender.At()} 已将 {number} 加入黑名单 ❥(ゝω・✿ฺ)");
                            blacklistConfig.Save();
                        }
                    }
                    else if (splitText[0] == "remove")
                    {
                        if (list.Contains(number))
                        {
                            list.Remove(number);
                            e.Source.Send($"{e.Sender.At()} 已将 {number} 移出黑名单 ❥(ゝω・✿ฺ)");
                            blacklistConfig.SetValueAndSave(new JArray(list));
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
