using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class SlappingApp : GroupApp
    {
        internal override string Name { get; } = "Slapping";

        internal override string DisplayName { get; } = "拍一拍";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new SlappingResetCommand(),
            new SlappingEditCommand(),
            new SlappingFeature()
        };
    }
}