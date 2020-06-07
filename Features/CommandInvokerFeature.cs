using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Command;

namespace Ricky8955555.CoolQ.Features
{
    class CommandInvokerFeature : Feature
    {
        public override void Invoke(MessageReceivedEventArgs e)
        {
            SplitMessage splitMessage = SplitMessage.Parse(e.Message);
            if (!splitMessage.IsEmpty)
            {
                string command = splitMessage.Command.ToLower();

                foreach (Command cmd in Main.Commands) // 遍历所有 App
                {
                    var app = cmd.App;

                    if (cmd.ResponseCommand == command) // 判断指令是否匹配
                    {
                        if (cmd.IsParameterRequired == ParameterRequiredOptions.Necessary == splitMessage.HasParameter ||
                        cmd.IsParameterRequired == ParameterRequiredOptions.Dispensable) // 判断指令中的参数有无与应用需求是否一致
                            app.Run(e, splitMessage.Parameter);
                        else if (cmd.IsParameterRequired != ParameterRequiredOptions.Dispensable)
                            e.Source.Send($"{e.Sender.At()} 该应用 {app.Name}（{app.DisplayName}）{(cmd.IsParameterRequired == ParameterRequiredOptions.Necessary ? "需要参数" : "无需参数")}，具体用法：{app.GetUsage()} (｀・ω・´)"); // 提示指令错误

                        e.Handled = true; // 该应用处理完毕，防止指令继续传递
                    }
                }
            }
        }
    }
}
