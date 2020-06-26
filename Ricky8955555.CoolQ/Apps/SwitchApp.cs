using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class SwitchApp : App
    {
        public override string Name { get; } = "Switch";

        public override string DisplayName { get; } = "应用开关";

        public override bool CanDisable { get; } = false;

        public override Permission Permission { get; } = Permission.Administrator;

        public override Feature[] Features { get; } = new Feature[]
        {
            new SwitchCommand()
        };
    }
}
