using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class SpacingApp : App
    {
        public override string Name { get; } = "Spacing";

        public override string DisplayName { get; } = "空格化";

        public override Feature[] Features { get; } = new Feature[]
        {
            new SpacingCommand()
        };
    }
}