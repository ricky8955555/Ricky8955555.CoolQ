using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using Ricky8955555.CoolQ.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;

namespace Ricky8955555.CoolQ.Features
{
    class AutoRepeaterFeature : Feature
    {
        public override void Invoke(MessageReceivedEventArgs e)
        {
            if (Chattables.Contains(e.Source))
                try
                {
                    e.Source.Send(e.Message);
                }
                catch (ApiException) { }
        }
    }
}
