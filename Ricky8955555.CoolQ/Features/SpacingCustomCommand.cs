using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class SpacingCustomCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "space";

        protected override string CommandUsage { get; } = "{0}space <空格数量> <文本> (自定义空格数量)";

        protected override LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.ComplexMessage;

        protected override bool IsHandledAutomatically { get; } = false;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText spaceNumberText, ComplexMessage elements)
        {
            var plainText = elements.OfType<PlainText>();

            if (plainText.Count() == elements.Count && int.TryParse(spaceNumberText, out int spaceNumber))
            {
                Handled = true;

                try
                {
                    string str = string.Join(" ", plainText);
                    e.Source.Send(string.Join(new string(' ', spaceNumber), str.ToCharArray()));
                }
                catch (ApiException ex) when (ex.ErrorCode == -26)
                {
                    e.Source.Send("发送出错了呀 (；´д｀)ゞ");
                }
            }
        }
    }
}