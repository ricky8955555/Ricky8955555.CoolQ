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

namespace Ricky8955555.CoolQ.Features
{
    class CommandInvokerFeature : Feature
    {
        readonly static List<IChattable> InitdChattables = new List<IChattable>();

        public override void Invoke(MessageReceivedEventArgs e)
        {
            SplitMessage splitMessage = SplitMessage.Parse(e.Message);
            if (!splitMessage.IsEmpty)
            {
                InitChattable(e.Source);
                var possibleApps = new ArrayList(); // 初始化 possibleApps(可能的应用) ArrayList
                string command = splitMessage.Command.ToLower();
                string sourceStr = e.Source.ToString();

                foreach (Command cmd in Main.Commands) // 遍历所有 App
                {
                    var app = cmd.App;

                    if (cmd.ResponseCommand == command) // 判断指令是否匹配
                    {
                        if (app.IsInternalEnabled && // 判断应用是否内部启用
                            ((!app.CanDisable) || // 判断应用是否可以被停用
                            Configs.AppConfig.Config[sourceStr][app.Name].ToObject<bool>())) // 判断应用是否启用
                        {
                            if (app.IsParameterRequired == App.ParameterRequiredOptions.Necessary == splitMessage.HasParameter ||
                            app.IsParameterRequired == App.ParameterRequiredOptions.Dispensable) // 判断指令中的参数有无与应用需求是否一致
                            {
                                if ((!app.IsForAdministrator) ||
                                    (app.IsForAdministrator && Configs.PluginConfig.Config["Administrator"].ToObject<long>() == e.Sender.Number)) // 判断该应用是否需要管理员权限，若是则判断对方是否为管理员
                                    app.Run(e, splitMessage.Parameter); // 调用应用
                                else
                                    e.Source.Send($"{e.Sender.At()} 该应用（{app.DisplayName}）仅限管理员使用 ┐(￣ヮ￣)┌");
                            }
                            else if (app.IsParameterRequired != App.ParameterRequiredOptions.Dispensable)
                                e.Source.Send($"该应用（{app.DisplayName}）{(app.IsParameterRequired == App.ParameterRequiredOptions.Necessary ? "需要参数" : "无需参数")} (｀・ω・´)"); // 提示指令错误

                            e.Handled = true; // 该应用处理完毕，防止指令继续传递
                        }
                        else if (!app.IsInternalEnabled)
                            e.Source.Send($"{e.Sender.At()} 很抱歉，该应用（{app.DisplayName}）已从内部停用 /(ㄒoㄒ)/~~");
                        else
                            e.Source.Send($"{e.Sender.At()} 很抱歉，该应用（{app.DisplayName}）已停用 /(ㄒoㄒ)/~~");
                    }
                    else if (command.Contains(cmd.ResponseCommand) || cmd.ResponseCommand.Contains(command)) // 判断是否有近似的命令
                        possibleApps.Add($"{cmd.App.Usage}  ->  {app.DisplayName}"); // 添加可能的命令到 possibleApps
                }

                if (possibleApps.Count > 0) // 判断是否存在可能的命令
                    e.Source.Send($"{e.Sender.At()} 没有这样的指令哟 ╰(￣▽￣)╭，可能你想用的指令是：\n" + string.Join("\n", possibleApps.ToArray())); // 输出可能的命令
            }
        }

        void InitChattable(IChattable source)
        {
            if (!InitdChattables.Contains(source))
            {
                string sourceStr = source.ToString();
                var appConfig = Configs.AppConfig;
                var config = (JObject)appConfig.Config;

                if (!config.ContainsKey(sourceStr))
                    config.Add(
                        new JProperty(
                            sourceStr,
                            new JObject()
                            { 
                                Main.Apps
                                .Where(x => x.CanDisable)
                                .Select(x => new JProperty(x.Name, x.IsEnabledByDefault))
                            })
                        );
                else
                {
                    var sourceConfig = (JObject)config[sourceStr];

                    foreach (App app in Main.Apps)
                    {
                        if (app.CanDisable && !sourceConfig.ContainsKey(app.Name))
                            sourceConfig.Add(new JProperty(app.Name, app.IsEnabledByDefault));
                        else if ((!app.CanDisable) && sourceConfig.ContainsKey(app.Name))
                            sourceConfig.Remove(app.Name);
                    }
                }

                appConfig.Save();
                InitdChattables.Add(source);
            }
        }
    }
}
