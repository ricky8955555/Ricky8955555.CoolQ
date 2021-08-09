using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class RandomApp : App
    {
        public override string Name { get; } = "Random";

        public override string DisplayName { get; } = "随机事件";

        public override Feature[] Features { get; } = new Feature[]
        {
            new RandomNumberCommand(),
            new RandomOptionsCommand()
        };
    }
}