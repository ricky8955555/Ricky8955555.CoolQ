using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Commands
{
    class PingCommand : Command
    {
        public override string ResponseCommand { get; } = "ping";
        public override App App { get; } = Main.Apps.Where(x => x.Name == "Ping").Single();
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Necessary;
    }
}
