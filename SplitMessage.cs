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
        public SplitMessage(string command, ComplexMessage parameter)
        {
            Command = command;
            Parameter = parameter;
            IsEmpty = false;
            HasParameter = true;
        }

        public SplitMessage(string command)
        {
            Command = command;
            IsEmpty = false;
        }

        public SplitMessage()
        {
        }

        public readonly string Command = string.Empty; // Command(命令) 为 String【默认值为 string.Empty】
        public readonly ComplexMessage Parameter = null; // Parameter(参数) 为 ComplexMessage【默认值为 null】
        public readonly bool IsEmpty = true; // IsEmpty(是否为空) 为 Boolean【默认值为 true】
        public readonly bool HasParameter = false; // HasParameter(是否有参数) 为 Boolean【默认值为 false】
    
        public static SplitMessage Parse(Message message)
        {
            var msg = message.Parse(); // 将 Message 转换成 ComplexMessage
            var currentUser = Bot.CurrentUser; // 定义 currentUser 为 当前用户

            if (msg.Contains(currentUser.At())) // 判断是否存在 At，且对象为机器人
            {
                if ((msg.TryDeconstruct(out At messageAt, out PlainText plainText, out ComplexMessage complexMessage) || // 情况 1：At 前面不存在任何字符串，且 At 后面有纯文本，纯文本后有其他内容
                msg.TryDeconstruct(out PlainText _, out messageAt, out plainText, out complexMessage)) && // 情况 2：At 前面不存在空纯文本，且 At 后面有纯文本，纯文本后有其他内容
                messageAt.Target == currentUser) // 确定 At 的目标为当前用户
                {
                    string[] plainTextSplit = plainText.Content.Trim().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (plainTextSplit.Length == 1) // 情况 a：纯文本中仅有命令
                        return new SplitMessage(plainTextSplit[0], new ComplexMessage { complexMessage }); // 返回带有命令及参数的 SplitMessage，且参数为纯文本后其他内容
                    else if (plainTextSplit.Length == 2) // 情况 b：纯文本中有命令及其他内容
                        return new SplitMessage(plainTextSplit[0], new ComplexMessage { plainTextSplit[1], complexMessage }); // 返回带有命令及参数的 SplitMessage，且参数为纯文本命令后内容
                }
                else if ((msg.TryDeconstruct(out messageAt, out plainText) || // 情况 3：At 前面不存在空纯文本，且 At 后面有纯文本，纯文本后无其他内容
                    msg.TryDeconstruct(out PlainText _, out messageAt, out plainText)) && // 情况 4：At 前面存在空纯文本，且 At 后面有纯文本，纯文本后无其他内容
                    messageAt.Target == currentUser) // 确定 At 的目标为当前用户
                {
                    string[] plainTextSplit = plainText.Content.Trim().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (plainTextSplit.Length == 1) // 情况 a：纯文本中仅有命令
                        return new SplitMessage(plainTextSplit[0]); // 返回仅带有命令的 SplitMessage，且参数为纯文本后其他内容
                    else if (plainTextSplit.Length == 2) // 情况 b：纯文本中有命令及其他内容
                        return new SplitMessage(plainTextSplit[0], new ComplexMessage { plainTextSplit[1] }); // 返回带有命令及参数的 SplitMessage，且参数为纯文本后其他内容
                }
            }

            return new SplitMessage(); // 返回空的 SplitMessage
        }
    }
}
