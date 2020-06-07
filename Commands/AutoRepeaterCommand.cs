using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Commands
{
    class AutoRepeaterCommand : Command
    {
        public override string ResponseCommand { get; } = "autorepeat";
        public override App App { get; } = Main.Apps.Where(x => x.Name == "AutoRepeater").Single();
    }
}
