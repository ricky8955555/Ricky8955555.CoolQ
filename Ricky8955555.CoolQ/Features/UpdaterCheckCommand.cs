using System;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class UpdaterCheckCommand : Command
    {
        public override string ResponseCommand { get; } = "update";

        protected override string CommandUsage { get; } = "{0}update (检查是否有版本更新)";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            var version = VersionUtilities.GetTagVersion();

            e.Reply(Resources.Processing);

            if (VersionUtilities.GetLatestVersion(out Version latestVersion, out _))
            {
                if (version < latestVersion)
                    e.Source.Send($"有新版本更新哦！(ૢ˃ꌂ˂⁎)\n当前版本：{version}\n最新 Release 版本：{latestVersion}\n\n如果需要更新，请发送 {Configurations.Prefix}update run");
                else if (version == latestVersion)
                    e.Source.Send($"当前版本已是最新版本！(ૢ˃ꌂ˂⁎)\n当前版本：{version}");
                else
                    e.Source.Send($"当前版本较最新 Release 版本新，可能你正在使用内测版本 (ﾉ´▽｀)ﾉ♪\n当前版本：{version}\n最新 Release 版本：{latestVersion}");
            }
            else
                e.Reply("获取最新版本信息失败了 (；´д｀)ゞ");
        }
    }
}