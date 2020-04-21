using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Apps;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ
{

    // Main 类
    [App("io.github.ricky8955555.addons")]
    class Main
    {
        public static App[] Apps { get; private set; } // 定义 Apps 为 App 数组
        public static Feature[] Features { get; private set; }

        public Main()
        {
            // 添加处理程序
            CurrentUser.MessageReceived += MessageReceived; // 接收信息事件
            Bot.AppEnabled += AppEnabled; // 程序启动事件
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
                ex.LogError();
            }
        }

        void AppEnabled(object sender, EventArgs e)
        {
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
                new RichTextAutoParserApp()
            };
        }
    }
}
