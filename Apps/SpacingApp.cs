using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class SpacingApp : App
    {
        public override string Name { get; } = "Spacing";

        public override string DisplayName { get; } = "空格化";

        public override Feature[] Features { get; } = new Feature[] {
            new SpacingCommand()
        };
    }
}