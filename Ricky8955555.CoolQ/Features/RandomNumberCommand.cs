using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    internal class RandomNumberCommand : Command<PlainText, PlainText, PlainText>
    {
        internal override string ResponseCommand { get; } = "random";

        protected override string CommandUsage { get; } = "{0}random number <最小值> <最大值> (输出随机数)\n" +
            "{0}random numberint <最小值> <最大值> (输出随机整数)\n";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText optionText, PlainText minValueText, PlainText maxValueText, ComplexMessage elements)
        {
            string option = optionText.Content.ToLower();

            if (option == "number")
            {
                if (double.TryParse(minValueText, out double minValue) && double.TryParse(maxValueText, out double maxValue))
                    e.Reply(RandomUtilities.NextDouble(minValue, maxValue).ToString());
            }
            else if (option == "numberint")
            {
                if (long.TryParse(minValueText, out long minValue) && long.TryParse(maxValueText, out long maxValue))
                    e.Reply(RandomUtilities.Next(minValue, maxValue).ToString());
            }
        }
    }
}