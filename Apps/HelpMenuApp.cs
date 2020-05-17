using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.QQ;
using HuajiTech.CoolQ.Messaging;
using HuajiTech.QQ.Events;

namespace Ricky8955555.CoolQ.Apps
{
    // HelpMenuApp 类 继承 App 类
    class HelpMenuApp : App
    {
        public override string Name { get; } = "HelpMenu";
        public override string DisplayName { get; } = "帮助菜单";
        public override string Command { get; } = "help";
        public override string Usage { get; } = "help";
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Unnecessary;

        static readonly int MaxAppCountInSingleMessage = 6; // 定义单消息内显示最大应用数量
        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter)
        {
            var appInfos = Main.Apps.Select(app => $"{Bot.CurrentUser.At()} {app.Usage}  ->  {app.DisplayName} ({app.Name})"); // 生成应用列表         
            int splitCount = (int)Math.Ceiling((float)appInfos.Count() / MaxAppCountInSingleMessage); // 计算分开消息数量

            // for 循环发送多条消息
            for (int i = 0; i < splitCount; i++)
            {
                int start = i * MaxAppCountInSingleMessage; // 起始 Index
                int count = i < splitCount - 1 ? MaxAppCountInSingleMessage : appInfos.Count() - start; // 计算该条消息应用信息数量
                var appInfosSplit = appInfos.Skip(start).Take(count); // 该条消息的应用列表
                e.Source.Send(string.Join("\n", appInfosSplit)); // 发送消息
            }
        }
    }
}
