using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Parsing;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    internal class CommandMessage
    {
        public string Command { get; }

        public ComplexMessage Parameters { get; }

        public CommandMessage(string command)
        {
            var splitter = new CommandMessageSplitter();
            var splitCommand = splitter.Split(command);
            Command = splitCommand[0];

            if (splitCommand.Count > 1)
                Parameters = splitCommand.Skip(1).Select(x => ComplexMessage.Parse(x).ToMessageElement()).ToComplexMessage();
            else
                Parameters = null;
        }
    }
}