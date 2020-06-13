using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using Ricky8955555.CoolQ.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class RichTextAutoParserApp : App
    {
        public override string Name { get; } = "RichTextAutoParser";

        public override string DisplayName { get; } = "富文本自动解析器";

        public override Feature[] Features { get; } = new Feature[] {
            new RichTextAutoParserFeature()
        };
    }
}
