using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    internal class MarketingCommand : Command<PlainText, PlainText, PlainText>
    {
        internal override string ResponseCommand { get; } = "marketing";

        protected override string CommandUsage { get; } = "{0}marketing <主体> <事件> <另一种说法>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText subject, PlainText eventText, PlainText anotherSaying, ComplexMessage elements)
        {
            e.Source.Send(string.Format(Resources.MarketingTemplate, subject, eventText, anotherSaying));
        }
    }
}