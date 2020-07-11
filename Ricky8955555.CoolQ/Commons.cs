using HuajiTech.CoolQ;
using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    internal static class Commons
    {
        internal const string AppId = "io.github.ricky8955555.addons";

        internal readonly static AppBase[] Apps = new AppBase[]
        {
            new HelpMenuApp(),
            new MusicApp(),
            new AboutApp(),
            new CovidStatusApp(),
            new SpacingApp(),
            new PingApp(),
            new RichTextAutoParserApp(),
            new SwitchApp(),
            new BlacklistManagerApp(),
            new AutoRepeaterApp(),
            new FakeMentionParserApp(),
            new SlappingApp(),
            new MarketingApp(),
            new ConfigApp()
        };

        internal static class Configs
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
}
