using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class HelpMenuCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "help";

        protected override string CommandUsage { get; } = "{0}help <页码>\n" +
            "{0}help <应用名称>";

        private static readonly int MaxCount = 10;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            var apps = AppUtilities.GetApps(e.Source, e.Sender);

            if (int.TryParse(plainText, out int pageIndex))
            {
                var appInfos = apps.Select(GetAppInfo);
                int pageCount = (int)Math.Ceiling((float)appInfos.Count() / MaxCount);

                if (pageIndex <= pageCount)
                {
                    int start = (pageIndex - 1) * MaxCount;
                    int count = pageIndex < pageCount ? MaxCount : appInfos.Count() - start;
                    var appInfosSplit = appInfos.Skip(start).Take(count);
                    e.Source.Send($"帮助菜单 (第 {pageIndex} 页 / 共 {pageCount} 页)：\n\n" + string.Join("\n", appInfosSplit));
                }
                else if (pageIndex < 1)
                    e.Reply($"页数不能小于等于 0 (Ｔ▽Ｔ)");
                else if (pageIndex > pageCount)
                    e.Reply($"帮助菜单仅有 {pageCount} 页 |ω･`)");
            }
            else
            {
                try
                {
                    var app = apps.Where(x => x.Name.ToLower() == plainText).Single();
                    e.Source.Send(GetAppInfo(app) + "：\n" + string.Join("\n", app.Features.Where(f => f.Usage != null).Select(f => f.Usage)));
                }
                catch (InvalidOperationException)
                {
                    e.Reply($"应用 {plainText} 不存在 (•́へ•́╬)");
                }
            }

            string GetAppInfo(AppBase app) => $"{(app.IsEnabled(e.Source) ? string.Empty : "【已停用】")}{app.DisplayName} ({app.Name})";
        }
    }
}