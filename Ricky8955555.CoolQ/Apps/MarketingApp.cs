using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class MarketingApp : App
    {
        public override string Name { get; } = "Marketing";

        public override string DisplayName { get; } = "营销号文章生成器";

        public override Feature[] Features { get; } = new Feature[]
        {
            new MarketingCommand()
        };
    }
}