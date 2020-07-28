using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;

namespace Ricky8955555.CoolQ.Features
{
    internal class UpdaterCommand : Command
    {
        protected override string CommandUsage { get; } = "{0}update (检查是否有版本更新)\n" +
            "{0}update run (运行版本更新)";

        internal override string ResponseCommand { get; } = "update";

        protected override bool CanHaveParameter { get; } = true;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            var version = VersionUtilities.GetTagVersion();

            if (elements == null)
            {
                e.Reply(Resources.Processing);

                if (GetLatestVersion(out Version latestVersion, out _))
                {
                    if (version < latestVersion)
                        e.Source.Send($"有新版本更新哦！(ૢ˃ꌂ˂⁎)\n当前版本：{version}\n最新 Release 版本：{latestVersion}\n\n如果需要更新，请发送 {Configuration.Prefix}update run");
                    else if (version == latestVersion)
                        e.Source.Send($"当前版本已是最新版本！(ૢ˃ꌂ˂⁎)\n当前版本：{version}");
                    else
                        e.Source.Send($"当前版本较最新 Release 版本新，可能你正在使用内测版本 (ﾉ´▽｀)ﾉ♪\n当前版本：{version}\n最新 Release 版本：{latestVersion}");
                }
                else
                    e.Reply("获取最新版本信息失败了 (；´д｀)ゞ");
            }
            else if (elements.Count == 1 && elements[0] is PlainText plainText)
            {
                e.Reply(Resources.Processing);

                string str = plainText.Content.ToLower();

                if (str == "run")
                {
                    if (GetLatestVersion(out Version latestVersion, out string downloadUri))
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
                else
                    NotifyIncorrectUsage(e);
            }
            else
                NotifyIncorrectUsage(e);
        }

        private static bool GetLatestVersion(out Version version, out string downloadUri)
        {
            version = null;
            downloadUri = null;

            try
            {
                if (HttpUtilities.HttpGet(Resources.VersionInfoURL, out string content))
                {
                    var json = JObject.Parse(content);
                    var versionJson = json["version"];
                    version = new Version(versionJson["major"].ToObject<int>(), versionJson["minor"].ToObject<int>(), versionJson["build"].ToObject<int>());
                    downloadUri = json["download"].ToString();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}