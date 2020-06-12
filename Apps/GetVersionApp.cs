using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Diagnostics;
using static HuajiTech.CoolQ.PluginContext;

namespace Ricky8955555.CoolQ.Apps
{
    
    class GetVersionApp : App
    {
        public override string Name { get; } = "GetVersion";
        public override string DisplayName { get; } = "获取插件版本";
        public override string Usage { get; } = "{0}version";

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
#if DEBUG
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(Main).Assembly.Location).FileVersion; 
            var sdkAssembly = Current.Packer.GetPackedAssemblies().Where(x => x.Name == "HuajiTech.CoolQ").Single(); 
            e.Source.Send($"插件版本：{fileVersion}-debug\nSDK版本：{sdkAssembly.Version}"); 
#else
            var assembly = Assembly.GetExecutingAssembly(); 
            var version = assembly.GetName().Version; 
            e.Source.Send($"插件版本：{version.ToString()}-release"); 
#endif

        }
    }
}
