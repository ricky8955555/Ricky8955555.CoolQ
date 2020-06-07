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
    abstract class App
    {
        public abstract string Name { get; } // Name(应用名称) 为 String 且 必须重写
        public abstract string DisplayName { get; } // DisplayName(显示应用名称) 为 String 且 必须重写
        public abstract string Usage { get; } 
        public virtual bool IsInternalEnabled { get; } = true; // IsInternalEnabled(是否内部启用) 为 Boolean 且 可重写【默认值为 true】
        public virtual bool IsEnabledByDefault { get; } = true; // IsEnabledByDefault(是否默认启用) 为 Boolean 且 可重写【默认值为 true】
        public virtual bool IsForAdministrator { get; } = false; // IsForAdministrator(是否为管理员应用) 为 Boolean 且 可重写【默认值为 false】
        public virtual bool CanDisable { get; } = true;  // Candisable(能否被停用) 为 Boolean 且 可重写【默认值为 true】
        protected abstract void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null); // Invoke(调用) 为 void 且 必须重写

        public void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            if (IsInternalEnabled && // 判断应用是否内部启用
            ((!CanDisable) || // 判断应用是否可以被停用
            Configs.AppConfig.Config[e.Source.ToString(true)][Name].ToObject<bool>())) // 判断应用是否启用
            {
                if ((!IsForAdministrator) ||
                    (IsForAdministrator && Configs.PluginConfig.Config["Administrator"].ToObject<long>() == e.Sender.Number)) // 判断该应用是否需要管理员权限，若是则判断对方是否为管理员
                    Invoke(e, parameter); // 调用应用
                else
                    e.Source.Send($"{e.Sender.At()} 该应用 {Name}（{DisplayName}）仅限管理员使用 ┐(￣ヮ￣)┌");
            }
            else if (!IsInternalEnabled)
                e.Source.Send($"{e.Sender.At()} 很抱歉，该应用 {Name}（{DisplayName}）已从内部停用 /(ㄒoㄒ)/~~");
            else
                e.Source.Send($"{e.Sender.At()} 很抱歉，该应用 {Name}（{DisplayName}）已停用 /(ㄒoㄒ)/~~");
        }

        public void NotifyIncorrectUsage(MessageReceivedEventArgs e)
        {
            e.Source.Send($"{e.Sender.At()} 参数错误 (￣３￣)a ，具体用法：{GetUsage()}");
        }

        public string GetUsage()
        {
            return string.Format(Usage, Configs.PluginConfig.Config["Prefix"]);
        }
    }
}
