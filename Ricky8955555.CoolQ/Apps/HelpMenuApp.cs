using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class HelpMenuApp : App
    {
        internal override string Name { get; } = "HelpMenu";

        internal override string DisplayName { get; } = "帮助菜单";

        internal override bool CanDisable { get; } = false;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new HelpMenuCommand()
        };
    }
}