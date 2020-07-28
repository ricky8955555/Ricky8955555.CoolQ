using Ricky8955555.CoolQ.Apps;

namespace Ricky8955555.CoolQ
{
    internal partial class AppBase
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
            new MarketingApp(),
            new ConfigApp(),
            new IPInfoApp(),
            new RandomApp(),
            new WeatherApp(),
            new UpdaterApp()
        };
    }
}