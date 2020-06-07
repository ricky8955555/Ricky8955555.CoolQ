using HuajiTech.CoolQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ
{
    abstract class AppFeature : Feature
    {
        public abstract App App { get; }

        protected abstract void Invokes(MessageReceivedEventArgs e);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            if (Configs.AppConfig.Config[e.Source.ToString(true)][App.Name].ToObject<bool>())
                Invokes(e);
        }
    }
}
