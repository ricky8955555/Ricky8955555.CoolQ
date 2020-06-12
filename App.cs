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
    abstract class App
    {
        public abstract string Name { get; } 
        public abstract string DisplayName { get; } 
        public abstract string Usage { get; } 
        public virtual bool IsInternalEnabled { get; } = true; 
        public virtual bool IsEnabledByDefault { get; } = true; 
        public virtual bool IsForAdministrator { get; } = false; 
        public virtual bool CanDisable { get; } = true;  
        protected abstract void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null); 

        public void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            if (IsInternalEnabled && 
            ((!CanDisable) || 
            Configs.AppConfig.Config[e.Source.ToString(true)][Name].ToObject<bool>())) 
            {
                if ((!IsForAdministrator) ||
                    (IsForAdministrator && Configs.PluginConfig.Config["Administrator"].ToObject<long>() == e.Sender.Number)) 
                    Invoke(e, parameter); 
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
