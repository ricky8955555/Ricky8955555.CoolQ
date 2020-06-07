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
    class AutoRepeaterFeature : AppFeature
    {
        public override App App { get; } = Main.Apps.Where(x => x.Name == "AutoRepeater").Single();

        protected override void Invokes(MessageReceivedEventArgs e)
        {
            if (Chattables.Contains(e.Source))
                ExtRun(e);
        }
    }
}
