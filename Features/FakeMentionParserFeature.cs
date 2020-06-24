using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    class FakeMentionParserFeature : Feature
    {
        public override string Usage { get; } = "当消息中存在假Mention，则返回真Mention";

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = PlainText.Unescape(e.Message);

            if (message.Contains("@"))
            {
                var mentions = ((IGroup)e.Source).GetMembers().Where(x => message.Contains("@" + x.DisplayName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Mention().ToSendableString());
                if (mentions.Any())
                    e.Source.Send(string.Join(" ", mentions));
            }
        }
    }
}
