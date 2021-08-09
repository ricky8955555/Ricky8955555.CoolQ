using System;
using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class FakeMentionParserFeature : Feature
    {
        public override string Usage { get; } = "当消息中存在纯文本形式的假提及，则发送提及";

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = e.Message;

            if (message.Contains("@"))
            {
                var mentions = ((IGroup)e.Source).GetMembers().Where(x => message.Contains("@" + x.DisplayName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Mention().ToSendableString());
                if (mentions.Any())
                    e.Source.Send(string.Join(" ", mentions));
            }
        }
    }
}