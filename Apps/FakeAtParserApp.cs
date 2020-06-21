using HuajiTech.CoolQ.Events;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class FakeAtParserApp : GroupApp
    {
        public override string Name { get; } = "FakeAtParser";

        public override string DisplayName { get; } = "假At解析器";

        public override bool IsEnabledByDefault { get; } = false;

        public override Feature[] Features { get; } = new Feature[]
        {
            new FakeAtParserFeature()
        };
    }
}
