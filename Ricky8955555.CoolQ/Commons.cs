using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    internal static class Commons
    {
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
            new MarketingApp()
        };

        internal readonly static Configuration PluginConfig = new PluginConfig();

        internal readonly static Configuration AppStatusConfig = new AppStatusConfig();

        internal readonly static Configuration BlacklistConfig = new BlacklistConfig();

        internal static string Prefix => PluginConfig.Config["Prefix"].ToString();

        internal static long Owner => PluginConfig.Config["Owner"].ToObject<long>();
    }
}
