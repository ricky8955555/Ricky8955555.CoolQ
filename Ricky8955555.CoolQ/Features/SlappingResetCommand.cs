using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ.Features
{
    public class SlappingResetCommand : Command
    {
        public override string ResponseCommand { get; } = "slapping";

        protected override string CommandUsage { get; } = "{0}slapping (重置)";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            SlappingConfig.Remove(e.Subject.Number);
            SlappingConfig.Save();

            e.Reply("你的拍一拍自定义语句重置好了 |ू･ω･` )");
        }
    }
}