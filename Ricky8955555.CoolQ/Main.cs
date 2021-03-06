﻿using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using static Ricky8955555.CoolQ.Configuration;

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
            //if (e.Source.Number == 706172242 && e.Message.Content.StartsWith("/"))
            //{
            //    var command = new CommandMessage(e.Message.Content.Trim().Substring(1));
            //    e.Source.Send($"Command: {command.Command}\nParameters: {string.Join("[[[", command.Parameters.Select(x => x.ToSendableString()))}");
            //}

            try
            {
                InitChattable(e.Source);

                if (!(BlacklistConfig.Config.ToObject<List<long>>().Contains(e.Subject.Number) || string.IsNullOrEmpty(e.Message)))
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

        private static void InitChattable(IChattable source)
        {
            if (!InitdChattables.Contains(source))
            {
                string sourceStr = source.ToString(true);

                var config = (JObject)AppStatusConfig.Config;
                config.Add(new JProperty(sourceStr, new JObject()), false);

                var sourceConfig = (JObject)config[sourceStr];
                var newConfig = new JObject();
                var apps = AppUtilities.GetApps(source);

                foreach (AppBase app in apps)
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