using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;

namespace Ricky8955555.CoolQ.Features
{
    internal class MarketingCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "marketing";

        protected override string CommandUsage { get; } = "{0}marketing <主体> <事件> <另一种说法>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            string str = plainText;
            string[] splitText = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (splitText.Length == 3)
                e.Source.Send(string.Format(Resources.MarketingTemplate, splitText));
            else
                NotifyIncorrectUsage(e);
        }
    }
}