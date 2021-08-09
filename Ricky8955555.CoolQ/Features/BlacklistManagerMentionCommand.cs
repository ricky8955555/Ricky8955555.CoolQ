using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static HuajiTech.CoolQ.CurrentPluginContext;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ.Features
{
    public class BlacklistManagerMentionCommand : Command<PlainText, Mention>
    {
        public override string ResponseCommand { get; } = "blacklist";

        protected override string CommandUsage { get; } = "{0}blacklist <add/remove> <提及>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText operationText, Mention mention, ComplexMessage elements)
        {
            var config = BlacklistConfig;
            long number = mention.TargetNumber;
            bool? operation = operationText.Content.ToLower().ToBool("add", "remove");

            if (number == Owner || number == Bot.CurrentUser.Number)
            {
                e.Reply("无法将主人或机器人加入到黑名单 ─=≡Σ(((つ•̀ω•́)つ");
            }
            else if (operation.HasValue)
            {
                if (operation.Value)
                {
                    e.Reply(config.Add(number) ? $"已将 {number} 加入黑名单 ❥(ゝω・✿ฺ)" : $"{number} 已存在黑名单内 (ц｀ω´ц*)");
                }
                else
                {
                    e.Reply(config.Remove(number) ? $"已将 {number} 移出黑名单 ❥(ゝω・✿ฺ)" : $"{number} 不存在黑名单内 (ц｀ω´ц*)");
                }

                config.Save();
            }
        }
    }
}