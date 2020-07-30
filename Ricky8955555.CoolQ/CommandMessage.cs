using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Parsing;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    internal class CommandMessage
    {
        public static CommandMessage Empty = new CommandMessage();

        public string Command { get; } = null;

        public ComplexMessage Parameters { get; } = null;

        public CommandMessage()
        {
        }

        public CommandMessage(string command)
        {
            var splitter = new CommandMessageSplitter();
            var splitCommand = splitter.Split(command);
            
            if (splitCommand.Count == 1)
            {
                Command = splitCommand[0];
                Parameters = null;
            }
            else if (splitCommand.Count > 1)
            {
                Command = splitCommand[0];
                Parameters = splitCommand.Skip(1).Select(x => ComplexMessage.Parse(x).ToMessageElement()).ToComplexMessage();
            }
        }
    }
}