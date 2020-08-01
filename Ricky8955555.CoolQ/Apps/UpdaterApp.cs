using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class UpdaterApp : App
    {
        internal override string Name { get; } = "Updater";

        internal override string DisplayName { get; } = "更新";

        internal override bool CanDisable { get; } = false;

        internal override AppPermission Permission { get; } = AppPermission.Owner;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new UpdaterCheckCommand(),
            new UpdaterRunCommand()
        };
    }
}