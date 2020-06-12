using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Apps
{
    class HelpMenuApp : App
    {
        public override string Name { get; } = "HelpMenu";
        public override string DisplayName { get; } = "帮助菜单";
        public override string Usage { get; } = "{0}help";
        public override bool CanDisable { get; } = false;

        static readonly int MaxCount = 6; 
        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string prefix = Configs.PluginConfig.Config["Prefix"].ToString();
            var appInfos = Main.Apps
                .Select(x => $"{$"{x.GetUsage()}  ->  {x.DisplayName} ({x.Name})"}{(x.IsForAdministrator ? "【管理员应用】" : string.Empty)}")
                .OrderBy(x => x); 
            int splitCount = (int)Math.Ceiling((float)appInfos.Count() / MaxCount); 

            
            for (int i = 0; i < splitCount; i++)
            {
                int start = i * MaxCount; 
                int count = i < splitCount - 1 ? MaxCount : appInfos.Count() - start; 
                var appInfosSplit = appInfos.Skip(start).Take(count); 
                e.Source.Send(string.Join("\n", appInfosSplit)); 
            }
        }
    }
}
