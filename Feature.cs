using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;

namespace Ricky8955555.CoolQ
{
    abstract class Feature
    {
        public abstract void Invoke(MessageReceivedEventArgs e);
    }
}
