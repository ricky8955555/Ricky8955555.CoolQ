using System;
using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    public class Main
    {
        private readonly List<IChattable> InitdChattables = new();

        public Main(ICurrentUserEventSource currentUserEventSource)
        {
            currentUserEventSource.AddMessageReceivedEventHandler(MessageReceived);
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                InitChattable(e.Source);

                if (!(BlacklistConfig.Config.ToObject<long[]>().Contains(e.Subject.Number) || string.IsNullOrEmpty(e.Message)))
                    foreach (var app in AppBase.Apps.OrderBy(x => (int)x.Priority))
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
                ex.LogAsError();
                e.Source.Send(string.Format(Resources.ExceptionNotification, Resources.ProjectURL, Resources.CommunityURL, ex));
            }
        }

        private void InitChattable(IChattable source)
        {
            if (!InitdChattables.Contains(source))
            {
                string sourceStr = source.ToUniversalString();

                var config = (JObject)AppStatusConfig.Config;

                if (!config.ContainsKey(sourceStr))
                {
                    config.Add(new JProperty(sourceStr, new JObject()));
                }

                var sourceConfig = (JObject)config[sourceStr];
                var newConfig = new JObject();
                var apps = AppUtilities.GetApps(source);

                foreach (AppBase app in apps)
                {
                    if (app.CanDisable)
                        newConfig.Add(new JProperty(app.Name, sourceConfig.ContainsKey(app.Name) ? sourceConfig[app.Name] : app.IsEnabledByDefault));
                }

                sourceConfig.Replace(newConfig);

                AppStatusConfig.Save();
                InitdChattables.Add(source);
            }
        }
    }
}