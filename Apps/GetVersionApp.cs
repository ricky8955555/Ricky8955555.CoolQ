using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Apps
{
    // GetVersionApp 类 继承 App 类
    class GetVersionApp : App
    {
        public override string Name { get; } = "GetVersion";
        public override string DisplayName { get; } = "获取插件版本";
        public override string Command { get; } = "version";
        public override string Usage { get; } = "version";

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter)
        {
#if DEBUG
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(Main).Assembly.Location).FileVersion; // 获取文件版本号
            e.Source.Send($"插件版本：{fileVersion}-debug"); // 发送版本号
#else
            var assembly = Assembly.GetExecutingAssembly(); // 获取程序集
            var version = assembly.GetName().Version; // 获取程序集版本号
            e.Source.Send($"插件版本：{version.ToString()}-release"); // 发送版本号
#endif

        }
    }
}
