using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;

namespace Ricky8955555.CoolQ.Features
{
    internal class UpdaterRunCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "update";

        protected override string CommandUsage { get; } = "{0}update run (运行版本更新)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, ComplexMessage elements)
        {
            e.Reply(Resources.Processing);

            var version = VersionUtilities.GetTagVersion();

            string str = plainText.Content.ToLower();

            if (str == "run")
            {
                if (VersionUtilities.GetLatestVersion(out Version latestVersion, out string downloadUri))
                {
                    if (version < latestVersion)
                    {
                        try
                        {
                            if (HttpUtilities.HttpDownload(downloadUri, $"app\\{Constants.AppId}.cpk"))
                                e.Reply("更新完毕，请手动重载应用 (*๓´╰╯`๓)");
                            else
                                e.Reply($"更新失败，发送 {Configuration.Prefix}update run 重试 (；´д｀)ゞ");
                        }
                        catch
                        {
                            e.Reply("下载插件失败了 (；´д｀)ゞ");
                        }
                    }
                    else
                        e.Source.Send("目前版本无需更新 (ﾉ´▽｀)ﾉ♪");
                }
                else
                    e.Reply("获取最新版本信息失败了 (；´д｀)ゞ");
            }
        }
    }
}
