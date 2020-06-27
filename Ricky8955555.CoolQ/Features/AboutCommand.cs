using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Reflection;

namespace Ricky8955555.CoolQ.Features
{
    class AboutCommand : Command
    {
        public override string ResponseCommand { get; } = "about";

        protected override string CommandUsage { get; } = "{0}about";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            string sdkVersion = SdkInfo.Version;
#if DEBUG
            string versionStr = version + " (Debug)";
#else
            string versionStr = version + " (Release)";
#endif
            e.Source.Send(string.Format(Resources.About, Resources.ProjectURL, Resources.SDKProjectURL, Resources.CoolQURL) + $"\n\n插件版本：{versionStr}\nSDK版本：{sdkVersion}");
        }
    }
}
