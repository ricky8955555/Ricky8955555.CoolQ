using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Configurations
{
    class PluginConfig : Configuration
    {
        public override JToken InitInfo { get; } = new JObject() 
        { 
            new JProperty("Prefix", "*"),
            new JProperty("Administrator", 0)
        };
        public override string Name { get; } = "PluginConfig";
    }
}
