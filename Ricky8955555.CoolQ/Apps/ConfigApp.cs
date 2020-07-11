using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class ConfigApp : App
    {
        internal override string Name { get; } = "Config";

        internal override string DisplayName { get; } = "配置";

        internal override bool CanDisable { get; } = false;

        internal override AppPriority Priority { get; } = AppPriority.Highest;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new ConfigCommand(),
            new ConfigFeature()
        };
    }
}
