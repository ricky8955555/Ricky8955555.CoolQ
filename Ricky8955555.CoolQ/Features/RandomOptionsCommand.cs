using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    internal class RandomOptionsCommand : Command<PlainText, PlainText>
    {
        internal override string ResponseCommand { get; } = "random";

        protected override string CommandUsage { get; } = "{0}random options <选项> (\"|\"为分隔符) (输出随机选项)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText optionText, PlainText options)
        {
            string option = optionText.Content.ToLower();

            if (option == "options")
                e.Reply(RandomUtilities.RandomOption(options.Content.Split('|')));
        }
    }
}
