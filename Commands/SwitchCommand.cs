using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Commands
{
    class SwitchCommand : Command
    {
        public override string ResponseCommand => "switch";
        public override App App => Main.Apps.Where(x => x.Name == "Switch").Single();
    }
}
