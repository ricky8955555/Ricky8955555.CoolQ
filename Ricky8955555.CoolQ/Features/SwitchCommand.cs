﻿using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class SwitchCommand : Command<PlainText, PlainText>
    {
        internal override string ResponseCommand { get; } = "switch";

        protected override string CommandUsage { get; } = "{0}switch <应用名称> <on/off>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText appName, PlainText operationText)
        {
            try
            {
                var config = Configuration.AppStatusConfig;
                var app = AppUtilities.GetApps(e.Source, e.Sender).Where(x => x.Name.ToLower() == appName.Content.ToLower()).Single();
                bool? operation = operationText.Content.ToLower().ToBool("on", "off");

                if (app.CanDisable)
                {
                    if (!operation.HasValue)
                        NotifyIncorrectUsage(e);
                    else
                    {
                        config.Config[e.Source.ToString(true)][app.Name] = operation.Value;
                        e.Reply($"已{(operation.Value ? "启用" : "停用")}应用 {app.DisplayName}（{app.Name}） ✧(≖ ◡ ≖✿ ");
                        config.Save();
                    }
                }
                else
                    e.Reply($"该应用 {app.DisplayName}（{app.Name}）不允许被启用/停用 o(ﾟДﾟ)っ！");
            }
            catch (InvalidOperationException)
            {
                e.Reply($"应用 {appName} 不存在 (•́へ•́╬)");
            }
        }
    }
}