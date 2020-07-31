using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using static Ricky8955555.CoolQ.Apps.ConfigApp;
using static Ricky8955555.CoolQ.Configuration;
using static Ricky8955555.CoolQ.Constants;

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
                if (CurrentUser == null && (Owner == -1 || e.Subject.Number == Owner))
                    CurrentUser = e.Subject;

                if (CurrentUser != null && CurrentUser.Equals(e.Subject))
                {
                    switch (CurrentStepId)
                    {
                        case 0:
                            if (Owner == -1)
                                e.Reply("Hey! 别来无恙啊，欢迎使用 Minop Bot!");
                            e.Reply("请输入管理员账号：（输入 -1 则设置为当前账号）");
                            break;

                        case 1:
                            if (long.TryParse(e.Message, out OwnerSet))
                            {
                                if (OwnerSet < -1)
                                {
                                    e.Reply("QQ 号不正确，请重新输入管理员账号：");
                                    CurrentStepId--;
                                    break;
                                }

                                e.Reply("管理员设置完毕！");
                                e.Reply("请输入命令响应前缀：");
                            }
                            else
                            {
                                e.Reply("获取 QQ 号失败，请重新输入管理员账号：");
                                CurrentStepId--;
                            }

                            break;

                        case 2:
                            Prefix = e.Message;
                            e.Reply("命令响应前缀设置完毕！");
                            break;
                    }

                    if (CurrentStepId == LastStepId)
                    {
                        e.Reply("配置准备就绪，敬请使用吧！\n" +
                            $"如果需要使用帮助菜单，请输入 {Prefix}help\n" +
                            $"如果设置管理员账号有误，请删除 data\\app\\{AppId}\\PluginConfig.json，并重载应用，发送 minop config\n" +
                            $"如果仅需修改其他内容，发送 {Prefix}config 重新配置即可");

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