using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class AutoRepeaterApp : App
    {
        public override string Name { get; } = "AutoRepeater";
        public override string DisplayName { get; } = "自动复读";
        public override string Usage { get; } = "{0}autorepeat";
        public override bool IsEnabledByDefault { get; } = false;

        public readonly static List<IChattable> Chattables = new List<IChattable>();

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            if (Chattables.Switchs(e.Source))
                e.Source.Send("已开启自动复读 ヾ(^Д^*)/");
            else
                e.Source.Send("已关闭自动复读 ヾ(^Д^*)/");
        }

        public static void ExtRun(MessageReceivedEventArgs e)
        {
            try
            {
                e.Source.Send(e.Message);
            }
            catch (ApiException) { }
        }
    }
}
