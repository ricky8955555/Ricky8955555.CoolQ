using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Diagnostics;
using static Ricky8955555.CoolQ.FeatureResources.GetVersionResources;

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
            e.Source.Send(string.Format(Debugging, fileVersion, sdkVersion));
#else
            var assembly = Assembly.GetExecutingAssembly(); 
            var version = assembly.GetName().Version;
            e.Source.Send(string.Format(Released, version, sdkVersion)); 
#endif

        }
    }
}
