using System;
using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ.Plugins
{
    [Plugin((int)AppLifecycle.Enabled)]
    class AppRunnerPlugin : Plugin 
    {
        readonly static List<IChattable> InitdChattables = new List<IChattable>();

        public AppRunnerPlugin (IMessageEventSource messageEventSource)
        {
            messageEventSource.AddMessageReceivedEventHandler(MessageReceived);
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                InitChattable(e.Source);
                if (!BlacklistConfig.Config.ToObject<List<long>>().Contains(e.Sender.Number))
                    foreach (var app in Commons.Apps)
                        app.Run(e);
            }
            catch (Exception ex)
            {
                ex.LogAsError().SendTo(e.Source);
            }
        }

        void InitChattable(IChattable source)
        {
            if (!InitdChattables.Contains(source))
            {
                string sourceStr = source.ToString(true);
                var config = (JObject)AppConfig.Config;

                if (!config.ContainsKey(sourceStr))
                    config.Add(
                        new JProperty(sourceStr, new JObject() { Commons.Apps.Where(x => x.CanDisable).Select(x => new JProperty(x.Name, x.IsEnabledByDefault)) }));
                else
                {
                    var sourceConfig = (JObject)config[sourceStr];

                    foreach (AppBase app in Commons.Apps)
                    {
                        if (app.CanDisable && !sourceConfig.ContainsKey(app.Name))
                            sourceConfig.Add(new JProperty(app.Name, app.IsEnabledByDefault));
                        else if ((!app.CanDisable) && sourceConfig.ContainsKey(app.Name))
                            sourceConfig.Remove(app.Name);
                    }
                }

                AppConfig.Save();
                InitdChattables.Add(source);
            }
        }
    }
}
