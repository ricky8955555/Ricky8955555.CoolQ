using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Configurations
{
    class BlacklistConfig : Configuration
    {
        public override string Name { get; } = "BlacklistConfig";
        protected override JToken InitInfo { get; } = new JArray();
    }
}
