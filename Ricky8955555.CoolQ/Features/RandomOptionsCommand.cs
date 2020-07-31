using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class RandomOptionsCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "random";

        protected override string CommandUsage { get; } = "{0}random options <选项> (空格为分隔符) (输出随机选项)";

        protected override LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.ComplexMessage;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText optionText, ComplexMessage elements)
        {
            var options = elements.OfType<PlainText>();

            if (optionText.Content.ToLower() == "options" && options.Count() == elements.Count)
                e.Reply((ISendable)RandomUtilities.RandomOption(options));
        }
    }
}