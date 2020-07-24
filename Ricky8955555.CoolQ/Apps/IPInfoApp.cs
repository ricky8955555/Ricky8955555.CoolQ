using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class IPInfoApp : App
    {
        internal override string Name { get; } = "IPInfo";

        internal override string DisplayName { get; } = "IP 信息查询";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new IPInfoCommand()
        };
    }
}