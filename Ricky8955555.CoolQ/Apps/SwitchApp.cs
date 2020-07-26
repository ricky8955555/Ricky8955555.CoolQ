using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class SwitchApp : App
    {
        internal override string Name { get; } = "Switch";

        internal override string DisplayName { get; } = "应用开关";

        internal override bool CanDisable { get; } = false;

        internal override AppPermission Permission { get; } = AppPermission.Administrator;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new SwitchCommand()
        };
    }
}