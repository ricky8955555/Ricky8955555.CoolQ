using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class CovidStatusApp : App
    {
        public override string Name { get; } = "CovidStatus";

        public override string DisplayName { get; } = "疫情动态";

        public override Feature[] Features { get; } = new Feature[] {
            new CovidStatusCommand()
        };
    }
}
