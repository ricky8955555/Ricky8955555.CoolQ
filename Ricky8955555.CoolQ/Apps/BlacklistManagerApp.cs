using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class BlacklistManagerApp : App
    {
        internal override string Name { get; } = "BlacklistManager";

        internal override string DisplayName { get; } = "黑名单管理器";

        internal override bool CanDisable { get; } = false;

        internal override AppPermission Permission { get; } = AppPermission.Owner;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new BlacklistManagerCommand()
        };
    }
}