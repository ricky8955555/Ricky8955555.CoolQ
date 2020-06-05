using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Commands
{
    class SpacingCommand : Command
    {
        public override string ResponseCommand { get; } = "space";
        public override App App { get; } = Main.Apps.Where(x => x.Name == "Spacing").Single();
    }
}
