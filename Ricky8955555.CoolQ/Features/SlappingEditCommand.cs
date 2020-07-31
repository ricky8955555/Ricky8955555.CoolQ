using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Configuration;

namespace Ricky8955555.CoolQ.Features
{
    internal class SlappingEditCommand : Command<MultipartElement>
    {
        internal override string ResponseCommand { get; } = "slapping";

        protected override string CommandUsage { get; } = "{0}slapping <自定义语句> (设置)";

        protected override void Invoking(MessageReceivedEventArgs e, MultipartElement element, ComplexMessage elements)
        {
            ((JObject)SlappingConfig.Config).Add(new JProperty(e.Subject.Number.ToString(), element.ToSendableString()), true);
            SlappingConfig.Save();

            e.Reply("你的拍一拍自定义语句设置好了 |ू･ω･` )");
        }
    }
}