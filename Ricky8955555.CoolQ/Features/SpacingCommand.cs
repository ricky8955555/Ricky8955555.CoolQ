using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class SpacingCommand : Command
    {
        internal override string ResponseCommand { get; } = "space";

        protected override string CommandUsage { get; } = "{0}space <文本> (空格数量为 3)";

        protected override LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.ComplexMessage;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            try
            {
                var plainText = elements.OfType<PlainText>();
                if (plainText.Count() == elements.Count)
                {
                    string str = string.Join(" ", plainText);
                    e.Source.Send(string.Join(new string(' ', 3), str.ToCharArray()));
                }
            }
            catch (ApiException ex) when (ex.ErrorCode == -26)
            {
                e.Source.Send("发送出错了呀 (；´д｀)ゞ");
            }
        }
    }
}