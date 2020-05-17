using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.QQ;
using HuajiTech.CoolQ.Messaging;
using HuajiTech.QQ.Events;

namespace Ricky8955555.CoolQ
{
    // App(应用) 类
    abstract class App : Plugin
    {
        public abstract string Name { get; } // Name(应用名称) 为 String 且 必须重写
        public abstract string DisplayName { get; } // DisplayName(显示应用名称) 为 String 且 必须重写
        public abstract string Command { get; } // Command(指令) 为 String 且 必须重写
        public virtual bool IsEnabled { get; } = true; // IsEnabled(是否启用) 为 Boolean 且 可重写【默认值为 true】
        public abstract string Usage { get; } // Usage(用法) 为 String 且 必须重写
        public virtual ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Dispensable; // IsParameterRequired(是否需要参数) 为 ParameterRequiredOptions 且 可重写【默认值为 Dispensable】
        public abstract void Run(MessageReceivedEventArgs e, ComplexMessage parameter); // Invoke(调用) 为 void 且 必须重写
        public enum ParameterRequiredOptions
        {
            Dispensable,
            Necessary,
            Unnecessary
        }
    }
}
