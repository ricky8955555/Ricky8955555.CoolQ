using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;

namespace Ricky8955555.CoolQ.Features
{
    class SpacingCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "space";

        protected override string CommandUsage { get; } = "{0}space [空格数量(缺省值 3)] <文本>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            string str = plainText;

            if (!string.IsNullOrEmpty(str))
            {
                string[] splitText = str.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    if (splitText.Length > 1 && int.TryParse(splitText[0], out int spaceNumber) && spaceNumber > 0)
                        e.Source.Send(string.Join(new string(' ', spaceNumber), splitText[1].ToCharArray()));
                    else
                        e.Source.Send(string.Join(new string(' ', 3), str.ToCharArray()));
                }
                catch (ApiException)
                {
                    e.Source.Send("发送出错了呀 (；´д｀)ゞ");
                }
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
