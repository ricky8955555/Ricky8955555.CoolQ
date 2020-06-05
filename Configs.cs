using Ricky8955555.CoolQ.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ
{
    class Configs
    {
        public readonly static Configuration PluginConfig = Main.Configurations.Where(x => x.Name == "PluginConfig").Single();
        public readonly static Configuration AppConfig = Main.Configurations.Where(x => x.Name == "AppConfig").Single();
    }
}
