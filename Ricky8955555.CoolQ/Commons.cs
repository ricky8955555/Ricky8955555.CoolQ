﻿using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    static class Commons
    {
        public readonly static AppBase[] Apps = new AppBase[]
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

        public readonly static Configuration PluginConfig = new PluginConfig();

        public readonly static Configuration AppConfig = new AppConfig();

        public readonly static Configuration BlacklistConfig = new BlacklistConfig();

        public static string Prefix => PluginConfig.Config["Prefix"].ToString();

        public static long Owner => PluginConfig.Config["Owner"].ToObject<long>();
    }
}