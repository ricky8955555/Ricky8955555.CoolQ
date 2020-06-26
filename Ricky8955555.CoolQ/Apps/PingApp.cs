using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class PingApp : App
    {
        public override string Name { get; } = "Ping";

        public override string DisplayName { get; } = "Ping";

        public override Feature[] Features { get; } = new Feature[]
        {
            new PingCommand()
        };
    }
}
