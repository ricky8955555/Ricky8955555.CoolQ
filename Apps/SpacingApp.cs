﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.QQ;
using HuajiTech.CoolQ.Messaging;
using HuajiTech.QQ.Events;

namespace Ricky8955555.CoolQ.Apps
{
    class SpacingApp : App
    {
        public override string Name { get; } = "Spacing";
        public override string DisplayName { get; } = "空格化";
        public override string Command { get; } = "space";
        public override string Usage { get; } = "space [空格数量(缺省值 3)] <文本>";
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Necessary;

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter)
        {
            string plainText = parameter.GetPlainText();

            if (!string.IsNullOrEmpty(plainText))
            {
                string[] splitText = plainText.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    if (splitText.Length > 1 && int.TryParse(splitText[0], out int spaceNumber) && spaceNumber > 0)
                        e.Source.Send(string.Join(new string(' ', spaceNumber), splitText[1].ToCharArray()));
                    else
                        e.Source.Send(string.Join(new string(' ', 3), plainText.ToCharArray()));
                }
                catch (ApiException)
                {
                    e.Source.Send("发送出错了呀 (；´д｀)ゞ");
                }
            }
            else
                e.Source.Send($"{e.Sender.At()} 参数错误 (￣３￣)a ，具体用法：{Usage}"); // 提示参数错误
        }
    }
}
