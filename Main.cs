using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.QQ;
using HuajiTech.QQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Features;
using System.Net.NetworkInformation;

namespace Ricky8955555.CoolQ
{

    // Main 类
    class Main : Plugin
    {
        public static App[] Apps { get; private set; } // 定义 Apps 为 App 数组
        public static Feature[] Features { get; private set; } // 定义 Features 为 Feature 数组

        public Main(IMessageEventSource messageEventSource)
        {
            // 添加处理程序
            messageEventSource.MessageReceived += MessageReceived; // 接收信息事件

            // 初始化 Feature
            Features = new Feature[]
            {
                new AppRunnerFeature(),
                new RichTextAutoParserFeature()
            };

            // 初始化 App
            Apps = new App[]
            {
                new HelpMenuApp(),
                new MusicApp(),
                new GetVersionApp(),
                new CovidStatusApp(),
                new SpacingApp(),
                new PingApp()
            };
        }

        void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                foreach (var feature in Features)
                    feature.Invoke(e);
            }
            catch (Exception ex)
            {
                ex.LogAsError();
            }
        }
    }
}
