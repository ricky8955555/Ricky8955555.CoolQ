﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class RichTextAutoParserApp : App
    {
        public override string Name { get; } = "RichTextAutoParser";
        public override string DisplayName { get; } = "富文本自动解析器（目前只支持部分）";
        public override string Command { get; } = "rtap";
        public override string Usage { get; } = "rtap";

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter)
        {
            if (RichTextAutoParserFeature.Switch((Group)e.Source)) // 判断程序是否启用并反转应用启用状态
                e.Source.Send("富文本自动解析器 已关闭 (๑＞ڡ＜)☆"); // 提示应用已关闭
            else
                e.Source.Send("富文本自动解析器 已启用 (๑＞ڡ＜)☆"); // 提示应用已启用
        }
    }
}
