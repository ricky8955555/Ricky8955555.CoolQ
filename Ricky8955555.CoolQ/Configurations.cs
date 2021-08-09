using Ricky8955555.CoolQ.Configs;

namespace Ricky8955555.CoolQ
{
    public static class Configurations
    {
        public readonly static PluginConfig PluginConfig = new();

        public readonly static AppStatusConfig AppStatusConfig = new();

        public readonly static BlacklistConfig BlacklistConfig = new();

        public readonly static SlappingConfig SlappingConfig = new();

        public static string Prefix
        {
            get => PluginConfig.Config["Prefix"].ToString();
            set => PluginConfig.Config["Prefix"] = value;
        }

        public static long Owner
        {
            get => PluginConfig.Config["Owner"].ToObject<long>();
            set => PluginConfig.Config["Owner"] = value;
        }
    }
}
