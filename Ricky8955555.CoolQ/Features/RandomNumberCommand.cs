using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class RandomNumberCommand : Command<PlainText, PlainText, PlainText>
    {
        public override string ResponseCommand { get; } = "random";

        protected override string CommandUsage { get; } = "{0}random number <最小值> <最大值> (输出随机数)\n" +
            "{0}random numberint <最小值> <最大值> (输出随机整数)";

        protected override bool IsHandledAutomatically { get; } = false;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText optionText, PlainText minValueText, PlainText maxValueText, ComplexMessage elements)
        {
            string option = optionText.Content.ToUpperInvariant();

            if (option is "NUMBER")
            {
                if (double.TryParse(minValueText, out double minValue) && double.TryParse(maxValueText, out double maxValue))
                {
                    e.Reply(RandomUtilities.NextDouble(minValue, maxValue).ToString());
                    Handled = true;
                }
            }
            else if (option is "NUMBERINT")
            {
                if (long.TryParse(minValueText, out long minValue) && long.TryParse(maxValueText, out long maxValue))
                {
                    e.Reply(RandomUtilities.Next(minValue, maxValue).ToString());
                    Handled = true;
                }
            }
        }
    }
}