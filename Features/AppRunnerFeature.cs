using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    class AppRunnerFeature : Feature
    {
        public override void Invoke(MessageReceivedEventArgs e)
        {
            SplitMessage splitMessage = SplitMessage.Parse(e.Message);
            if (!splitMessage.IsEmpty)
            {
                var possibleApps = new ArrayList(); // 初始化 possibleApps(可能的应用) ArrayList

                foreach (App app in Main.Apps) // 遍历所有 App
                    if (app.Command == splitMessage.Command && // 判断指令是否匹配
                        app.IsEnabled) // 判断应用是否启用
                    {
                        if (app.IsParameterRequired == splitMessage.HasParameter) // 判断指令中的参数有无与应用需求是否一致
                            app.Run(e, splitMessage.Parameter); // 调用应用
                        else
                            e.Source.Send(app.IsParameterRequired ? $"{e.Sender.At()} 该程序（{app.DisplayName}）需要参数 (￣３￣)a ，具体用法：{app.Usage}" : $"{e.Sender.At()} 该程序（{app.DisplayName}）无需参数 (￣３￣)a ，具体用法：{app.Usage}"); // 提示指令错误

                        e.Handled = true; // 该应用处理完毕，防止指令继续传递
                    }
                    else if (splitMessage.Command.Contains(app.Command) || app.Command.Contains(splitMessage.Command)) // 判断是否有近似的命令
                        possibleApps.Add($"{app.Usage}  ->  {app.DisplayName}"); // 添加可能的命令到 possibleApps

                if (possibleApps.Count > 0) // 判断是否存在可能的命令
                    e.Source.Send($"{e.Sender.At()} 没有这样的指令哟 ╰(￣▽￣)╭，可能你想用的指令是：\n" + string.Join("\n", possibleApps.ToArray())); // 输出可能的命令
            }
        }
    }
}
