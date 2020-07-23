using HuajiTech.CoolQ.Events;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class FakeMentionParserApp : GroupApp
    {
        internal override string Name { get; } = "FakeMentionParser";

        internal override string DisplayName { get; } = "假提及解析器";

        internal override bool IsEnabledByDefault { get; } = false;

        internal override Feature[] Features { get; } = new Feature[]
        {
            new FakeMentionParserFeature()
        };
    }
}