using Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    internal partial class Configuration
    {
        internal readonly static Configuration PluginConfig = new PluginConfig();

        internal readonly static Configuration AppStatusConfig = new AppStatusConfig();

        internal readonly static Configuration BlacklistConfig = new BlacklistConfig();

        internal readonly static Configuration SlappingConfig = new SlappingConfig();

        internal static string Prefix
        {
            get => PluginConfig.Config["Prefix"].ToString();
            set => PluginConfig.Config["Prefix"] = value;
        }

        internal static long Owner
        {
            get => PluginConfig.Config["Owner"].ToObject<long>();
            set => PluginConfig.Config["Owner"] = value;
        }
    }
}
