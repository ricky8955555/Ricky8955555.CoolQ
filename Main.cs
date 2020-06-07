﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Commands;
using Ricky8955555.CoolQ.Configurations;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ
{

    // Main 类
    class Main : Plugin
    {
        public static App[] Apps { get; private set; } // 定义 Apps 为 App 数组
        public static Command[] Commands { get; private set; } // 定义 Commands 为 Command 数组
        public static Feature[] Features { get; private set; } // 定义 Features 为 Feature 数组
        public static Configuration[] Configurations { get; private set; } // 定义 Configurations 为 Configuration 数组

        readonly static List<IChattable> InitdChattables = new List<IChattable>();

        public Main(INotifyMessageReceived notifyMessageReceived)
        {
            // 添加处理程序
            notifyMessageReceived.MessageReceived += OnMessageReceived; // 接收信息事件

            // 初始化 App
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
                new TestApp(),
                new BlacklistManagerApp(),
                new AutoRepeaterApp()
            };

            // 初始化 Commands
            Commands = new Command[]
            {
                new HelpMenuCommand(),
                new MusicCommand(),
                new GetVersionCommand(),
                new CovidStatusCommand(),
                new SpacingCommand(),
                new PingCommand(),
                new SwitchCommand(),
                new TestCommand(),
                new BlacklistManagerCommand(),
                new AutoRepeaterCommand()
            };

            // 初始化 Feature
            Features = new Feature[]
            {
                new CommandInvokerFeature(),
                new RichTextAutoParserFeature(),
                new AutoRepeaterFeature()
            };

            //初始化 Configuration
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
                if (!Configs.BlacklistConfig.Config.ToObject<List<long>>().Contains(e.Sender.Number))
                    foreach (var feature in Features)
                        feature.Invoke(e);
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
                var appConfig = Configs.AppConfig;
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
