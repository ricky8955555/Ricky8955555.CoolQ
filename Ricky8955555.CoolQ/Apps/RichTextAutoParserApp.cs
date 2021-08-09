using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class RichTextAutoParserApp : App
    {
        public override string Name { get; } = "RichTextAutoParser";

        public override string DisplayName { get; } = "富文本自动解析器";

        public override Feature[] Features { get; } = new Feature[]
        {
            new RichTextAutoParserFeature()
        };
    }
}