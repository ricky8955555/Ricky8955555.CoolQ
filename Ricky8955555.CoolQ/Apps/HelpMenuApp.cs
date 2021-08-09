using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class HelpMenuApp : App
    {
        public override string Name { get; } = "HelpMenu";

        public override string DisplayName { get; } = "帮助菜单";

        public override bool CanDisable { get; } = false;

        public override Feature[] Features { get; } = new Feature[]
        {
            new HelpMenuUsageCommand(),
            new HelpMenuCommand()
        };
    }
}