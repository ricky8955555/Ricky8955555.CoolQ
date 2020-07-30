using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    internal class SpacingCustomCommand : Command<PlainText, PlainText>
    {
        internal override string ResponseCommand { get; } = "space";

        protected override string CommandUsage { get; } = "{0}space <空格数量> <文本> (自定义空格数量)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText spaceNumberText, PlainText plainText)
        {
            if (int.TryParse(spaceNumberText, out int spaceNumber))
                try
                {
                    e.Source.Send(string.Join(new string(' ', spaceNumber), plainText.Content.ToCharArray()));
                }
                catch (ApiException ex) when (ex.ErrorCode == -26)
                {
                    e.Source.Send("发送出错了呀 (；´д｀)ゞ");
                }
        }
    }
}
