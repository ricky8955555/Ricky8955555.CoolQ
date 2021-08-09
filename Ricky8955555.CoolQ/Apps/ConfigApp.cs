using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class ConfigApp : App
    {
        public override string Name { get; } = "Config";

        public override string DisplayName { get; } = "配置";

        public override bool CanDisable { get; } = false;

        public override AppPriority Priority { get; } = AppPriority.Highest;

        public override AppPermission Permission { get; } = AppPermission.Owner;

        public override Feature[] Features { get; } = new Feature[]
        {
            new ConfigCommand(),
            new ConfigFeature()
        };

        public static bool IsRunning = false;
    }
}