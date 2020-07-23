using HuajiTech.CoolQ;
using Ricky8955555.CoolQ.Features;
using System.Collections.Generic;

namespace Ricky8955555.CoolQ.Apps
{
    class AutoRepeaterApp : App
    {
        internal override string Name { get; } = "AutoRepeater";

        internal override string DisplayName { get; } = "自动复读";

        internal override bool IsEnabledByDefault { get; } = false;

        internal override AppPriority Priority { get; } = AppPriority.Lowest;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new AutoRepeaterCommand(),
            new AutoRepeaterFeature()
        };

        internal readonly static List<IChattable> Chattables = new List<IChattable>();
    }
}