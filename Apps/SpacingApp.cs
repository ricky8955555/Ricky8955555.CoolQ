using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Apps
{
    class SpacingApp : App
    {
        public override string Name { get; } = "Spacing";
        public override string DisplayName { get; } = "空格化";
        public override string Usage { get; } = "{0}space [空格数量(缺省值 3)] <文本>";

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
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
                NotifyIncorrectUsage(e); 
        }
    }
}