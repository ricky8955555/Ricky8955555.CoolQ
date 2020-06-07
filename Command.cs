using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ
{
    abstract class Command
    {
        public abstract string ResponseCommand { get; }
        public abstract App App { get; }
        public virtual ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Dispensable; // IsParameterRequired(是否需要参数) 为 ParameterRequiredOptions 且 可重写【默认值为 Dispensable】

        public enum ParameterRequiredOptions
        {
            Dispensable,
            Necessary,
            Unnecessary
        }
    }
}
