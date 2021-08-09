using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class FakeMentionParserApp : GroupApp
    {
        public override string Name { get; } = "FakeMentionParser";

        public override string DisplayName { get; } = "假提及解析器";

        public override bool IsEnabledByDefault { get; } = false;

        public override Feature[] Features { get; } = new Feature[]
        {
            new FakeMentionParserFeature()
        };
    }
}