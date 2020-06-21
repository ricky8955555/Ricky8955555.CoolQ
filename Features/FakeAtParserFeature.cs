using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    class FakeAtParserFeature : Feature
    {
        public override string Usage { get; } = "当消息中存在假At，则返回真At";

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = e.Message.Content;

            if (message.Contains("@"))
            {
                var ats = ((IGroup)e.Source).GetMembers().Where(x => message.Contains("@" + x.DisplayName)).Select(x => x.At().ToSendableString());
                if (ats.Any())
                    e.Source.Send(string.Join(" ", ats));
            }
        }
    }
}
