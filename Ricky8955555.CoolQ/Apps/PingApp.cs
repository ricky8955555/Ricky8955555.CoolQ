using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class PingApp : App
    {
        internal override string Name { get; } = "Ping";

        internal override string DisplayName { get; } = "Ping";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new PingCommand()
        };
    }
}