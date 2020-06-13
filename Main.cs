using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Configurations;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ
{
    class Main : Plugin
    {
        public static App[] Apps { get; private set; }
        public static Configuration[] Configurations { get; private set; }

        readonly static List<IChattable> InitdChattables = new List<IChattable>();

        public Main(INotifyMessageReceived notifyMessageReceived)
        {
           
            notifyMessageReceived.MessageReceived += OnMessageReceived;

            Apps = new App[]
            {
                new HelpMenuApp(),
                new MusicApp(),
                new GetVersionApp(),
                new CovidStatusApp(),
                new SpacingApp(),
                new PingApp(),
                new RichTextAutoParserApp(),
                new SwitchApp(),
                new BlacklistManagerApp(),
                new AutoRepeaterApp()
            };

            Configurations = new Configuration[]
            {
                new PluginConfig(),
                new AppConfig(),
                new BlacklistConfig()
            };
        }

        void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                InitChattable(e.Source);
                if (!Common.BlacklistConfig.Config.ToObject<List<long>>().Contains(e.Sender.Number))
                    foreach (var app in Apps)
                        app.Run(e);
            }
            catch (ApiException ex)
            {
                ex.LogAsWarning();
            }
            catch (Exception ex)
            {
                ex.LogAsError();
                try
                {
                    e.Source.Send(ex.ToString());
                }
                catch (ApiException exa) 
                {
                    exa.LogAsWarning();
                }
            }
        }

        void InitChattable(IChattable source)
        {
            if (!InitdChattables.Contains(source))
            {
                string sourceStr = source.ToString(true);
                var appConfig = Common.AppConfig;
                var config = (JObject)appConfig.Config;

                if (!config.ContainsKey(sourceStr))
                    config.Add(
                        new JProperty(
                            sourceStr,
                            new JObject()
                            {
                                Apps
                                .Where(x => x.CanDisable)
                                .Select(x => new JProperty(x.Name, x.IsEnabledByDefault))
                            })
                        );
                else
                {
                    var sourceConfig = (JObject)config[sourceStr];

                    foreach (App app in Apps)
                    {
                        if (app.CanDisable && !sourceConfig.ContainsKey(app.Name))
                            sourceConfig.Add(new JProperty(app.Name, app.IsEnabledByDefault));
                        else if ((!app.CanDisable) && sourceConfig.ContainsKey(app.Name))
                            sourceConfig.Remove(app.Name);
                    }
                }

                appConfig.Save();
                InitdChattables.Add(source);
            }
        }
    }
}
