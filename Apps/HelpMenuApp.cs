using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Apps
{
    // HelpMenuApp 类 继承 App 类
    class HelpMenuApp : App
    {
        public override string Name { get; } = "HelpMenu";
        public override string DisplayName { get; } = "帮助菜单";
        public override string Usage { get; } = "{0}help";
        public override bool CanDisable { get; } = false;
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Unnecessary;

        static readonly int MaxCount = 6; // 定义单消息内显示最大应用数量
        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string prefix = Configs.PluginConfig.Config["Prefix"].ToString();
            var appInfos = Main.Apps
                .Select(x => $"{$"{string.Format(x.Usage, Configs.PluginConfig.Config["Prefix"])}  ->  {x.DisplayName} ({x.Name})"}{(x.IsForAdministrator ? "【管理员应用】" : string.Empty)}")
                .OrderBy(x => x); // 生成应用列表
            int splitCount = (int)Math.Ceiling((float)appInfos.Count() / MaxCount); // 计算分开消息数量

            // for 循环发送多条消息
            for (int i = 0; i < splitCount; i++)
            {
                int start = i * MaxCount; // 起始 Index
                int count = i < splitCount - 1 ? MaxCount : appInfos.Count() - start; // 计算该条消息应用信息数量
                var appInfosSplit = appInfos.Skip(start).Take(count); // 该条消息的应用列表
                e.Source.Send(string.Join("\n", appInfosSplit)); // 发送消息
            }
        }
    }
}
