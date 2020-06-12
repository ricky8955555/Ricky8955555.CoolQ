using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class PingApp : App
    {
        public override string Name { get; } = "Ping";
        public override string DisplayName { get; } = "Ping";
        public override string Usage { get; } = "{0}ping <IP 地址或域名>";

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            var ping = new Ping();
            long totalRoundtripTime = 0;
            int packetLostCount = 0;
            string plainText = parameter?[0] as PlainText;
            string[] splitText = plainText?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int pingCount = 4;

            if (splitText != null &&
                (splitText.Length == 2 && int.TryParse(splitText[1], out pingCount) && pingCount > 0) ||
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
                            e.Source.Send($"Ping {splitText[0]} 失败，失败原因：" + pingReply.Status.ToString());
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
