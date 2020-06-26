using HuajiTech.CoolQ;
using Ricky8955555.CoolQ.Features;
using System.Collections.Generic;

namespace Ricky8955555.CoolQ.Apps
{
    class AutoRepeaterApp : App
    {
        public override string Name { get; } = "AutoRepeater";

        public override string DisplayName { get; } = "自动复读";

        public override bool IsEnabledByDefault { get; } = false;

        public override Feature[] Features { get; } = new Feature[]
        {
            new AutoRepeaterCommand(),
            new AutoRepeaterFeature()
        };

        public readonly static List<IChattable> Chattables = new List<IChattable>();
    }
}
