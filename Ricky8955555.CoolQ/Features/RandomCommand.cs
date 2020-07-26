using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;

namespace Ricky8955555.CoolQ.Features
{
    internal class RandomCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "random";

        protected override string CommandUsage { get; } = "{0}random number <最小值> <最大值> (输出随机数)\n" +
            "{0}random numberint <最小值> <最大值> (输出随机整数)\n" +
            "{0}random options <选项> (空格为分隔符) (输出随机选项)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            string[] splitText = plainText.Content.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (splitText.Length == 2)
            {
                if (splitText[0] == "number")
                {
                    string[] numStrs = splitText[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (numStrs.Length == 2 && double.TryParse(numStrs[0], out double minValue) && double.TryParse(numStrs[1], out double maxValue))
                        e.Reply(RandomUtilities.NextDouble(minValue, maxValue).ToString());
                    else
                        NotifyIncorrectUsage(e);
                }
                else if (splitText[0] == "numberint")
                {
                    string[] numStrs = splitText[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (numStrs.Length == 2 && long.TryParse(numStrs[0], out long minValue) && long.TryParse(numStrs[1], out long maxValue))
                        e.Reply(RandomUtilities.Next(minValue, maxValue).ToString());
                    else
                        NotifyIncorrectUsage(e);
                }
                else if (splitText[0] == "options")
                    e.Reply(RandomUtilities.RandomOption(splitText[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}