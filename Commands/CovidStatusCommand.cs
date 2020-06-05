using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Commands
{
    class CovidStatusCommand : Command
    {
        public override string ResponseCommand { get; } = "covid-19";
        public override App App { get; } = Main.Apps.Where(x => x.Name == "CovidStatus").Single();
    }
}
