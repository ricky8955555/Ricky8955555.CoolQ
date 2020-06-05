using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Diagnostics;
using HuajiTech.CoolQ;

namespace Ricky8955555.CoolQ.Apps
{
    // GetVersionApp 类 继承 App 类
    class GetVersionApp : App
    {
        public override string Name { get; } = "GetVersion";
        public override string DisplayName { get; } = "获取插件版本";
        public override string Usage { get; } = "{0}version";
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Unnecessary;

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
#if DEBUG
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(Main).Assembly.Location).FileVersion; // 获取文件版本号
            var sdkAssembly = Context.Packer.GetPackedAssemblies().Where(x => x.Name == "HuajiTech.CoolQ").Single(); // 获取 SDK 程序集
            e.Source.Send($"插件版本：{fileVersion}-debug\nSDK版本：{sdkAssembly.Version}"); // 发送版本号
#else
            var assembly = Assembly.GetExecutingAssembly(); // 获取程序集
            var version = assembly.GetName().Version; // 获取程序集版本号
            e.Source.Send($"插件版本：{version.ToString()}-release"); // 发送版本号
#endif

        }
    }
}
