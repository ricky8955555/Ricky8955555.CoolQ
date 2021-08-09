using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class SlappingApp : GroupApp
    {
        public override string Name { get; } = "Slapping";

        public override string DisplayName { get; } = "拍一拍";

        public override Feature[] Features { get; } = new Feature[]
        {
            new SlappingResetCommand(),
            new SlappingEditCommand(),
            new SlappingFeature()
        };
    }
}