using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class HelpMenuApp : App
    {
        public override string Name { get; } = "HelpMenu";

        public override string DisplayName { get; } = "帮助菜单";

        public override bool CanDisable { get; } = false;

        public override Feature[] Features { get; } = new Feature[] {
            new HelpMenuCommand()
        };
    }
}
