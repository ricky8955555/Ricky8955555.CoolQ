using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;
using static Ricky8955555.CoolQ.Utilities;
using static Ricky8955555.CoolQ.Commons.Configs;

namespace Ricky8955555.CoolQ.Features
{
    class HelpMenuCommand : Command
    {
        internal override string ResponseCommand { get; } = "help";

        protected override string CommandUsage { get; } = "{0}help [页码 (缺省值 1)]";

        protected override bool CanHaveParameter { get; } = true;

        private static readonly int MaxCount = 6;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            string prefix = PluginConfig.Config["Prefix"].ToString();
            var appInfos = GetApps(e.Source, e.Sender).Select(x => $"{(x.IsEnabled(e.Source) ? string.Empty : "【已停用】")}{x.DisplayName} ({x.Name}):\n" + string.Join("\n", x.Features.Where(f => f.Usage != null).Select(f => f.Usage).OrderBy(f => f))).OrderBy(x => x);
            int pageCount = (int)Math.Ceiling((float)appInfos.Count() / MaxCount);
            int pageIndex = 1;

            if (elements == null ||
                (elements != null && elements.Count == 1 && elements[0] is PlainText plainText && int.TryParse(plainText.Content, out pageIndex)))
            {
                if (pageIndex <= pageCount)
                {
                    int start = (pageIndex - 1) * MaxCount;
                    int count = pageIndex < pageCount ? MaxCount : appInfos.Count() - start;
                    var appInfosSplit = appInfos.Skip(start).Take(count);
                    e.Source.Send($"帮助菜单 (第 {pageIndex} 页 / 共 {pageCount} 页)：\n" + string.Join("\n\n", appInfosSplit));
                }
                else if (pageIndex < 1)
                    e.Source.Send($"页数不能小于等于 0 (Ｔ▽Ｔ)");
                else if (pageIndex > pageCount)
                    e.Source.Send($"对于你而言，该帮助菜单仅有 {pageCount} 页 |ω･`)");
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
