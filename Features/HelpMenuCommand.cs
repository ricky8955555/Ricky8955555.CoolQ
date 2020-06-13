using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Features
{
    class HelpMenuCommand : Command
    {
        public override string ResponseCommand { get; } = "help";

        protected override string CommandUsage { get; } = "{0}help";

        static readonly int MaxCount = 6;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            string prefix = Common.PluginConfig.Config["Prefix"].ToString();
            var appInfos = Main.Apps
                .Select(x => $"{x.DisplayName} ({x.Name}){(x.IsForAdministrator ? "【管理员应用】" : string.Empty)}:\n" + 
                string.Join("\n",
                    x.Features
                    .Where(f => f.Usage != null)
                    .Select(f => f.Usage)
                    .OrderBy(f => f)))
                .OrderBy(x => x);
            int splitCount = (int)Math.Ceiling((float)appInfos.Count() / MaxCount);


            for (int i = 0; i < splitCount; i++)
            {
                int start = i * MaxCount;
                int count = i < splitCount - 1 ? MaxCount : appInfos.Count() - start;
                var appInfosSplit = appInfos.Skip(start).Take(count);
                e.Source.Send(string.Join("\n\n", appInfosSplit));
            }
        }
    }
}
