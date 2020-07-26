using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class RichTextAutoParserApp : App
    {
        internal override string Name { get; } = "RichTextAutoParser";

        internal override string DisplayName { get; } = "富文本自动解析器";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new RichTextAutoParserFeature()
        };
    }
}