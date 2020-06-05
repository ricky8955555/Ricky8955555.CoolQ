using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    // App(应用) 类
    abstract class App : Plugin
    {
        public abstract string Name { get; } // Name(应用名称) 为 String 且 必须重写
        public abstract string DisplayName { get; } // DisplayName(显示应用名称) 为 String 且 必须重写
        public abstract string Usage { get; } 
        public virtual bool IsInternalEnabled { get; } = true; // IsInternalEnabled(是否内部启用) 为 Boolean 且 可重写【默认值为 true】
        public virtual bool IsEnabledByDefault { get; } = true; // IsEnabledByDefault(是否默认启用) 为 Boolean 且 可重写【默认值为 true】
        public virtual bool IsForAdministrator { get; } = false; // IsForAdministrator(是否为管理员应用) 为 Boolean 且 可重写【默认值为 false】
        public virtual bool CanDisable { get; } = true;  // Candisable(能否被停用) 为 Boolean 且 可重写【默认值为 true】
        public virtual ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Dispensable; // IsParameterRequired(是否需要参数) 为 ParameterRequiredOptions 且 可重写【默认值为 Dispensable】
        public abstract void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null); // Invoke(调用) 为 void 且 必须重写

        public void NotifyIncorrectUsage(MessageReceivedEventArgs e)
        {
            e.Source.Send($"{e.Sender.At()} 参数错误 (￣３￣)a ，具体用法：{string.Format(Usage, Configs.PluginConfig.Config["Prefix"])}");
        }

        public enum ParameterRequiredOptions
        {
            Dispensable,
            Necessary,
            Unnecessary
        }
    }
}
