using System;
using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Commons.Configs;
using static Ricky8955555.CoolQ.Utilities;

namespace Ricky8955555.CoolQ
{
    internal class Main
    {
        private readonly static List<IChattable> InitdChattables = new List<IChattable>();

        internal Main(ICurrentUserEventSource currentUserEventSource)
        {
            currentUserEventSource.AddMessageReceivedEventHandler(MessageReceived);
        }

        private static void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                InitChattable(e.Source);

                if (!(BlacklistConfig.Config.ToObject<List<long>>().Contains(e.Sender.Number) || string.IsNullOrEmpty(e.Message)))
                    foreach (var app in Commons.Apps.OrderBy(x => (int)x.Priority))
                    {
                        app.Run(e);
                        if (app.Handled)
                        {
                            app.Handled = false;
                            break;
                        }
                    }
            }
            catch (ApiException ex) when (ex.ErrorCode == -34) { }
            catch (ApiException ex)
            {
                ex.LogAsWarning();
            }
            catch (Exception ex)
            {
                ex.LogAsError().SendTo(e.Source);
            }
        }

        private static void InitChattable(IChattable source)
        {
            if (!InitdChattables.Contains(source))
            {
                string sourceStr = source.ToString(true);

                var config = (JObject)AppStatusConfig.Config;
                config.Add(new JProperty(sourceStr, new JObject()), false);

                var sourceConfig = (JObject)config[sourceStr];
                var newConfig = new JObject();

                foreach (AppBase app in GetApps(source))
                {
                    if (app.CanDisable)
                        newConfig.Add(sourceConfig.ContainsKey(app.Name) ? new JProperty(app.Name, sourceConfig[app.Name]) : new JProperty(app.Name, app.IsEnabledByDefault));
                }

                sourceConfig.Replace(newConfig);

                AppStatusConfig.Save();
                InitdChattables.Add(source);
            }
        }
    }
}