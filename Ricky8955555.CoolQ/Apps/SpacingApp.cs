using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class SpacingApp : App
    {
        internal override string Name { get; } = "Spacing";

        internal override string DisplayName { get; } = "空格化";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new SpacingCommand()
        };
    }
}