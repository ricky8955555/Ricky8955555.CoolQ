using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class UpdaterApp : App
    {
        public override string Name { get; } = "Updater";

        public override string DisplayName { get; } = "更新";

        public override bool CanDisable { get; } = false;

        public override AppPermission Permission { get; } = AppPermission.Owner;

        public override Feature[] Features { get; } = new Feature[]
        {
            new UpdaterCheckCommand(),
            new UpdaterRunCommand()
        };
    }
}