using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class RandomApp : App
    {
        internal override string Name { get; } = "Random";

        internal override string DisplayName { get; } = "随机事件";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new RandomCommand()
        };
    }
}