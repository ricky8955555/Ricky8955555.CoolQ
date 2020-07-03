using System;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static HuajiTech.CoolQ.CurrentPluginContext;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ.Features
{
    class BlacklistManagerCommand : Command
    {
        internal override string ResponseCommand { get; } = "blacklist";

        protected override string CommandUsage { get; } = "{0}blacklist <add/remove> <提及/QQ号>";

        protected override bool CanHaveParameter { get; } = true;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            var config = (JArray)BlacklistConfig.Config;
            long number = long.MinValue;
            bool? operation = false;

            if (elements.Count == 1 && elements.TryDeconstruct(out PlainText plainText))
            {
                string[] splitText = plainText.Content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitText.Length == 2 && long.TryParse(splitText[1], out number))
                    operation = splitText[0].ToLower().ToBool("add", "remove");
            }
            else if (elements.Count == 2 && elements.TryDeconstruct(out plainText, out Mention mention))
            {
                operation = plainText.Content.Trim().ToLower().ToBool("add", "remove");
                number = mention.TargetNumber;
            }

            if (number == Owner || number == Bot.CurrentUser.Number)
                e.Reply("无法将主人或机器人加入到黑名单 ─=≡Σ(((つ•̀ω•́)つ");
            else
            {
                if ((!operation.HasValue) || number == long.MinValue)
                    NotifyIncorrectUsage(e);
                else if (operation.Value)
                {
                    if (config.Contains(number, true))
                        e.Reply($"{number} 已存在黑名单内 (ц｀ω´ц*)");
                    else
                    {
                        config.Add(number);
                        BlacklistConfig.Save();
                        e.Reply($"已将 {number} 加入黑名单 ❥(ゝω・✿ฺ)");
                    }
                }
                else
                {
                    if (config.Contains(number, true))
                    {
                        BlacklistConfig.SetValueAndSave(config.Remove(number, true));
                        e.Reply($"已将 {number} 移出黑名单 ❥(ゝω・✿ฺ)");
                    }
                    else
                        e.Reply($"{number} 不存在黑名单内 (ц｀ω´ц*)");
                }
            }
        }
    }
}
