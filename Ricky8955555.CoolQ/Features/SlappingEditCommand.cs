using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ.Features
{
    public class SlappingEditCommand : Command<MultipartElement>
    {
        public override string ResponseCommand { get; } = "slapping";

        protected override string CommandUsage { get; } = "{0}slapping <自定义语句> (设置)";

        protected override void Invoking(MessageReceivedEventArgs e, MultipartElement element, ComplexMessage elements)
        {
            SlappingConfig.SetSentence(e.Subject.Number, element.ToSendableString());
            SlappingConfig.Save();

            e.Reply("你的拍一拍自定义语句设置好了 |ू･ω･` )");
        }
    }
}