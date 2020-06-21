using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Diagnostics;

namespace Ricky8955555.CoolQ.Features
{
    class GetVersionCommand : Command
    {
        public override string ResponseCommand { get; } = "version";

        protected override string CommandUsage { get; } = "{0}version";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string sdkVersion = SdkInfo.Version;

#if DEBUG
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(Main).Assembly.Location).FileVersion;
            e.Source.Send($"插件版本：{fileVersion}-debug\nSDK版本：{sdkVersion}");
#else
            var assembly = Assembly.GetExecutingAssembly(); 
            var version = assembly.GetName().Version;
            e.Source.Send($"插件版本：{version.ToString()}-release\nSDK版本：{sdkVersion}"); 
#endif

        }
    }
}
