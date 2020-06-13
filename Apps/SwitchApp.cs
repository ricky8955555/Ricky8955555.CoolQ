using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using Ricky8955555.CoolQ.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class SwitchApp : App
    {
        public override string Name { get; } = "Switch";

        public override string DisplayName { get; } = "应用开关";

        public override bool CanDisable { get; } = false;

        public override bool IsForAdministrator { get; } = true;

        public override Feature[] Features { get; } = new Feature[] {
            new SwitchCommand()
        };
    }
}
