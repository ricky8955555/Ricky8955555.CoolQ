using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Commands
{
    class GetVersionCommand : Command
    {
        public override string ResponseCommand { get; } = "version";
        public override App App { get; } = Main.Apps.Where(x => x.Name == "GetVersion").Single();
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Unnecessary;
    }
}
