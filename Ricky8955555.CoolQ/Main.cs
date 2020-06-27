using System;
using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ
{
    public static partial class Main
    {
        private readonly static List<IChattable> InitdChattables = new List<IChattable>();

        private static void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                InitChattable(e.Source);
                if (!BlacklistConfig.Config.ToObject<List<long>>().Contains(e.Sender.Number))
                    foreach (var app in Commons.Apps)
                        app.Run(e);
            }
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

                var config = (JObject)AppConfig.Config;
                config.Add(new JProperty(sourceStr, new JObject()), false);

                var sourceConfig = (JObject)config[sourceStr];

                foreach (AppBase app in Commons.Apps)
                {
                    sourceConfig.Operate(new JProperty(app.Name, app.IsEnabledByDefault), false, app.CanDisable);
                }

                AppConfig.Save();
                InitdChattables.Add(source);
            }
        }
    }
}
