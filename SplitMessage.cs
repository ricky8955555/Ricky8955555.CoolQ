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

            if (msg.Contains(Bot.CurrentUser.At())) // 判断是否存在 At
            {
                int sIndex = GetAtInComplexMessageIndex(msg, out PlainText messagePlainText); // 尝试获取 At 在 ComplexMessage 的位置

                if (sIndex > -1 && msg.Count > sIndex + 1)
                {
                    string strPlainText = messagePlainText.Content; // 定义 strPlainText 为 ComplexMessage 中从 At 开始的第 1 组数据
                    string[] splitPlainText = strPlainText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // 以空格为分隔符，把 firstContent 中的内容分隔开，并删去多余空格

                    if (splitPlainText.Length == 1)
                        return new SplitMessage(splitPlainText[0].Trim().ToLower()); // 返回只有 Command(命令)、无 Parameter(参数) 的 SplitMessage
                    else if (splitPlainText.Length > 1)
                        return new SplitMessage(
                            splitPlainText[0].Trim().ToLower(),
                            new ComplexMessage
                            {
                                strPlainText.Substring(strPlainText.IndexOf(splitPlainText[0]) + splitPlainText[0].Length + 1), // 替换从 At 开始的第 1 组数据中前面包含 Command(命令) 的部分
                                Enumerable.Range(sIndex + 2, msg.Count - sIndex - 2).Select(x => msg.ElementAt(x)) // 返回只有 Command(命令)、有 Parameter(参数) 的 SplitMessage
                            });
                }
            }

            return new SplitMessage(); // 返回空的 SplitMessage
        }

        static int GetAtInComplexMessageIndex(ComplexMessage msg, out PlainText messagePlainText)
        {
            var currentUser = Bot.CurrentUser; // 定义 currentUser 为 当前用户

            if (msg.TryDeconstruct(out At messageAt, out messagePlainText) && messageAt.Target == currentUser)
                return 0; // 确定 At 位于 ComplexMessage 中的第 0 组

            else if (msg.TryDeconstruct(out PlainText messagePlainTextA, out messageAt, out messagePlainText) &&
                string.IsNullOrWhiteSpace(messagePlainTextA.Content) &&
                messageAt.Target == currentUser)
                return 1; // 确定 At 位于 ComplexMessage 中的第 1 组

            return -1; // 返回默认空值
        }
    }
}
