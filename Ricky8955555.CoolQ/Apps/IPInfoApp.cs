using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class IPInfoApp : App
    {
        public override string Name { get; } = "IPInfo";

        public override string DisplayName { get; } = "IP 信息查询";

        public override Feature[] Features { get; } = new Feature[]
        {
            new IPInfoCommand()
        };
    }
}