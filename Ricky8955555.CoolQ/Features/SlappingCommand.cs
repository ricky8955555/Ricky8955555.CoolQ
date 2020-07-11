using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Commons.Configs;

namespace Ricky8955555.CoolQ.Features
{
    internal class SlappingCommand : Command
    {
        protected override string CommandUsage { get; } = "{0}slapping <自定义语句>";

        internal override string ResponseCommand { get; } = "slapping";

        protected override bool CanHaveParameter { get; } = true;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            if (elements == null)
                NotifyIncorrectUsage(e);
            else
            {
                ((JObject)SlappingConfig.Config).Add(new JProperty(e.Sender.Number.ToString(), elements.ToSendableString()), true);
                SlappingConfig.Save();

                e.Reply("你的拍一拍自定义语句设置好了 |ू･ω･` )");
            }
        }
    }
}
