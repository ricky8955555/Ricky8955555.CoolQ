using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Diagnostics;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    
    class GetVersionApp : App
    {
        public override string Name { get; } = "GetVersion";

        public override string DisplayName { get; } = "获取插件版本";

        public override Feature[] Features { get; } = new Feature[] {
            new GetVersionCommand()
        };
    }
}
