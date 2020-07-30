using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    internal class SpacingCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "space";

        protected override string CommandUsage { get; } = "{0}space <文本> (空格数量为 3)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            try
            {
                e.Source.Send(string.Join(new string(' ', 3), plainText.Content.ToCharArray()));
            }
            catch (ApiException ex) when (ex.ErrorCode == -26)
            {
                e.Source.Send("发送出错了呀 (；´д｀)ゞ");
            }
        }
    }
}