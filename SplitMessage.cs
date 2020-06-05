using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    // SplitMessage 类
    class SplitMessage
    {
        SplitMessage(string command, ComplexMessage parameter)
        {
            Command = command;
            Parameter = parameter;
            IsEmpty = false;
            HasParameter = true;
        }

        SplitMessage(string command)
        {
            Command = command;
            IsEmpty = false;
        }

        SplitMessage()
        {
        }

        public readonly string Command = string.Empty; // Command(命令) 为 String【默认值为 string.Empty】
        public readonly ComplexMessage Parameter = null; // Parameter(参数) 为 ComplexMessage【默认值为 null】
        public readonly bool IsEmpty = true; // IsEmpty(是否为空) 为 Boolean【默认值为 true】
        public readonly bool HasParameter = false; // HasParameter(是否有参数) 为 Boolean【默认值为 false】
    
        public static SplitMessage Parse(IMessage message)
        {
            string msgStr = message.ToString().Trim();
            string prefix = Configs.PluginConfig.Config["Prefix"].ToString();

            if (msgStr.StartsWith(prefix))
            {
                string str = msgStr.Substring(prefix.Length);
                string[] splitStr = str.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (splitStr.Length == 1)
                    return new SplitMessage(str);
                else if (splitStr.Length == 2)
                    return new SplitMessage(splitStr[0], ComplexMessage.Parse(splitStr[1]));
            }
            return new SplitMessage(); // 返回空的 SplitMessage
        }
    }
}
