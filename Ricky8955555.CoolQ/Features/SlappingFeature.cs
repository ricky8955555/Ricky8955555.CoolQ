using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    class SlappingFeature : Feature
    {
        internal override string Usage { get; } = "当发送者单条消息中含有 2 个以上对同个人提及时，则发送 “@发送者 拍了拍 @提及者”";

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            var message = e.Message.Parse();
            var mentions = message.OfType<Mention>();
            var distinctMentions = mentions.Distinct();

            var slappeeMentions = distinctMentions.Where(x => mentions.Where(x.Equals).Count() > 2);

            if (slappeeMentions.Any())
                e.Reply("拍了拍 " + string.Join(" ", slappeeMentions));
        }
    }
}
