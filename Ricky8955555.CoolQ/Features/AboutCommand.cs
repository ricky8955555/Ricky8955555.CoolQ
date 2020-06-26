﻿using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Diagnostics;

namespace Ricky8955555.CoolQ.Features
{
    class AboutCommand : Command
    {
        public override string ResponseCommand { get; } = "about";

        protected override string CommandUsage { get; } = "{0}about";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(Main).Assembly.Location).FileVersion;
            string sdkVersion = SdkInfo.Version;
#if DEBUG
            string version = fileVersion + "-debug";
#else
            string version = fileVersion + "-release";
#endif
            e.Source.Send(string.Format(Resources.About, Resources.ProjectURL, Resources.SDKProjectURL, Resources.CoolQURL) + $"\n\n插件版本：{version}\nSDK版本：{sdkVersion}");
        }
    }
}
