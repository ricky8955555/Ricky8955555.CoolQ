using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Net.NetworkInformation;

namespace Ricky8955555.CoolQ.Features
{
    class PingCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "ping";

        protected override string CommandUsage { get; } = "{0}ping <IP 地址或域名> [次数 (缺省值 4)]";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            var ping = new Ping();
            long totalRoundtripTime = 0;
            int packetLostCount = 0;
            string[] splitText = plainText.Content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int pingCount = 4;

            if (splitText != null &&
                splitText.Length == 2 && int.TryParse(splitText[1], out pingCount) && pingCount > 0 ||
                splitText.Length == 1)
                try
                {
                    for (int i = 0; i < pingCount; i++)
                    {
                        var pingReply = ping.Send(splitText[0]);
                        if (pingReply.Status == IPStatus.Success)
                            totalRoundtripTime += pingReply.RoundtripTime;
                        else if (pingReply.Status == IPStatus.TimedOut)
                            packetLostCount += 1;
                        else
                        {
                            e.Source.Send($"Ping {splitText[0]} 失败，失败原因：{pingReply.Status}");
                            return;
                        }
                    }

                    if (packetLostCount < pingCount)
                        e.Source.Send($"Ping {splitText[0]} 结果：\n延迟：{totalRoundtripTime / pingCount} ms\n丢包率：{packetLostCount / pingCount * 100} %");
                    else
                        e.Source.Send($"Ping {splitText[0]} 超时");
                }
                catch (Exception ex)
                {
                    e.Source.Send($"Ping {splitText[0]} 出错，错误原因：" + ex.Message);
                }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
