using System;
using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class HelpMenuCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "help";

        protected override string CommandUsage { get; } = "{0}help <页码>\n" +
            "{0}help <应用名称>";

        private static readonly int MaxCount = 10;

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, ComplexMessage elements)
        {
            var apps = AppUtilities.GetApps(e.Source, e.Subject);

            if (int.TryParse(plainText, out int pageIndex))
            {
                var appInfos = apps.Select(GetAppInfo).OrderBy(x => x);
                int pageCount = (int)Math.Ceiling((float)appInfos.Count() / MaxCount);

                if (pageIndex > 0 && pageIndex <= pageCount)
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
                var app = apps.Where(x => x.Name.Equals(plainText.Content, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (app is null)
                {
                    e.Reply($"应用 {plainText} 不存在 (•́へ•́╬)");
                    return;
                }

                e.Source.Send(GetAppInfo(app) + "：\n" + string.Join("\n", app.Features.Where(f => f.Usage is not null).Select(f => f.Usage)));
            }

            string GetAppInfo(AppBase app) => $"{(app.IsEnabled(e.Source) ? string.Empty : "【已停用】")}{app.DisplayName} ({app.Name})";
        }
    }
}