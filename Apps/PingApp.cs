using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class PingApp : App
    {
        public override string Name { get; } = "Ping";

        public override string DisplayName { get; } = "Ping";

        public override Feature[] Features { get; } = new Feature[] {
            new PingCommand()
        };
    }
}
