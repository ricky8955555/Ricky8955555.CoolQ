using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class MusicApp : App
    {
        public override string Name { get; } = "Music";

        public override string DisplayName { get; } = "点歌";

        public override Feature[] Features { get; } = new Feature[] {
            new MusicCommand()
        };
    }
}
