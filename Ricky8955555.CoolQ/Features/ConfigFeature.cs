using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using System.Linq;
using static Ricky8955555.CoolQ.Apps.ConfigApp;
using static Ricky8955555.CoolQ.Commons;
using static Ricky8955555.CoolQ.Commons.Configs;

namespace Ricky8955555.CoolQ.Features
{
    internal class ConfigFeature : Feature
    {
        private static int CurrentStepId = 0;

        private static readonly int LastStepId = 2;

        private static IChattable CurrentUser = null;

        private static long OwnerSet = -1;

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            if (Owner == -1)
                Handled = true;

            if (IsRunning)
            {
                if (CurrentUser == null && (Owner == -1 || e.Sender.Number == Owner))
                    CurrentUser = e.Sender;

                if (CurrentUser != null && CurrentUser.Equals(e.Sender))
                {
                    switch (CurrentStepId)
                    {
                        case 0:
                            if (Owner == -1)
                                e.Source.Send("Hey! 别来无恙啊，欢迎使用 Minop Bot!");
                            e.Source.Send("请输入管理员账号：（输入 -1 则设置为当前账号）");
                            break;

                        case 1:
                            if (long.TryParse(e.Message, out OwnerSet))
                            {
                                if (OwnerSet < -1)
                                {
                                    e.Source.Send("QQ 号不正确，请重新输入管理员账号：");
                                    CurrentStepId--;
                                    break;
                                }

                                e.Source.Send("管理员设置完毕！");
                                e.Source.Send("请输入命令响应前缀：");
                            }
                            else
                            {
                                e.Source.Send("获取 QQ 号失败，请重新输入管理员账号：");
                                CurrentStepId--;
                            }

                            break;

                        case 2:
                            Prefix = e.Message;
                            e.Source.Send("命令响应前缀设置完毕！");
                            break;
                    }

                    if (CurrentStepId == LastStepId)
                    {
                        e.Source.Send("配置准备就绪，敬请使用吧！");
                        e.Source.Send($"如果需要使用帮助菜单，请输入 {Commons.Apps.Where(x => x.Name == "HelpMenu").Single().Features.Single().Usage}");
                        e.Source.Send($"如果设置有误，请删除 data\\app\\{AppId}\\PluginConfig.json，并重载应用，重新发送 {Usage}");

                        if (OwnerSet == -1)
                            Owner = CurrentUser.Number;
                        else
                            Owner = OwnerSet;

                        PluginConfig.Save();
                        CurrentStepId = 0;
                        CurrentUser = null;
                        OwnerSet = -1;
                        IsRunning = false;
                    }
                    else
                        CurrentStepId++;
                }

                Handled = true;
            }
        }
    }
}
