using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Reflection;
using static HuajiTech.CoolQ.CurrentPluginContext;
using static Ricky8955555.CoolQ.Utilities;

namespace Ricky8955555.CoolQ.Features
{
    class AboutCommand : Command
    {
        internal override string ResponseCommand { get; } = "about";

        protected override string CommandUsage { get; } = "{0}about";

        private readonly long RickyNumber = 397050061;

        private readonly long SYCNumber = 2761729667;

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
            string ricky = "Ricky8955555";
            string syc = "SYC";

            if (e.Source is IGroup group)
            {
                var rickyMember = Member(RickyNumber, group);
                var sycMember = Member(SYCNumber, group);

                try
                {
                    rickyMember.Request();
                    ricky = rickyMember.Mention().ToSendableString();
                }
                catch { }

                try
                {
                    sycMember.Request();
                    syc = sycMember.Mention().ToSendableString();
                }
                catch { }
            }

            e.Source.Send(string.Format(Resources.About, ricky, syc, Resources.ProjectURL, Resources.CommunityURL, Resources.SDKProjectURL, Resources.CoolQURL) + $"\n\n插件版本：{versionStr}\nSDK版本：{sdkVersion}\n编译时间：{ToDateTime(version)}");
        }
    }
}
