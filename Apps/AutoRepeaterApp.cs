using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class AutoRepeaterApp : App
    {
        public override string Name { get; } = "AutoRepeater";
        public override string DisplayName { get; } = "自动复读";
        public override bool IsEnabledByDefault { get; } = false;
        public override Feature[] Features { get; } = new Feature[] {
            new AutoRepeaterCommand(),
            new AutoRepeaterFeature()
        };

        public readonly static List<IChattable> Chattables = new List<IChattable>();
    }
}
