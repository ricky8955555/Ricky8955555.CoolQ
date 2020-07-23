using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class MarketingApp : App
    {
        internal override string Name { get; } = "Marketing";

        internal override string DisplayName { get; } = "营销号文章生成器";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new MarketingCommand()
        };
    }
}