using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class SpacingCustomCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "space";

        protected override string CommandUsage { get; } = "{0}space <空格数量> <文本> (自定义空格数量)";

        protected override LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.ComplexMessage;

        protected override bool IsHandledAutomatically { get; } = false;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText spaceNumberText, ComplexMessage elements)
        {
            var plainText = elements.OfType<PlainText>();

            if (plainText.Count() == elements.Count && int.TryParse(spaceNumberText, out int spaceNumber) && spaceNumber >= 0)
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