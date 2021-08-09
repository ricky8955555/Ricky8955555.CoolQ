using System.Collections.Generic;
using HuajiTech.CoolQ;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class AutoRepeaterApp : App
    {
        public override string Name { get; } = "AutoRepeater";

        public override string DisplayName { get; } = "自动复读";

        public override bool IsEnabledByDefault { get; } = false;

        public override AppPriority Priority { get; } = AppPriority.Lowest;

        public override Feature[] Features { get; } = new Feature[]
        {
            new AutoRepeaterCommand(),
            new AutoRepeaterFeature()
        };

        public readonly static List<IChattable> Chattables = new();
    }
}