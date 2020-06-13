using HuajiTech.CoolQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ
{
    class Common
    {
        public readonly static Configuration PluginConfig = Main.Configurations.Where(x => x.Name == "PluginConfig").Single();
        public readonly static Configuration AppConfig = Main.Configurations.Where(x => x.Name == "AppConfig").Single();
        public readonly static Configuration BlacklistConfig = Main.Configurations.Where(x => x.Name == "BlacklistConfig").Single();

        public static string Prefix => PluginConfig.Config["Prefix"].ToString();
        public static long Administrator => PluginConfig.Config["Administrator"].ToObject<long>();
    }
}
