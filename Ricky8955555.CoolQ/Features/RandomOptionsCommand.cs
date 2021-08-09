using System;
using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class RandomOptionsCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "random";

        protected override string CommandUsage { get; } = "{0}random options <选项> (空格为分隔符) (输出随机选项)";

        protected override LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.ComplexMessage;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText optionText, ComplexMessage elements)
        {
            var options = elements.OfType<PlainText>();

            if (optionText.Content.Equals("options", StringComparison.OrdinalIgnoreCase) && options.Count() == elements.Count)
                e.Reply((ISendable)RandomUtilities.RandomOption(options));
        }
    }
}