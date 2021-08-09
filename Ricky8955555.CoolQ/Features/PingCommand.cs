using System;
using System.Linq;
using System.Net.NetworkInformation;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class PingCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "ping";

        protected override string CommandUsage { get; } = "{0}ping <IP 地址或域名> (Ping 4 次取平均值)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, ComplexMessage elements)
        {
            e.Reply(Resources.Processing);

            try
            {
                var results = PingUtilities.SendMoreAndGetRoundtripTime(plainText, 4);
                var successResults = results.Where(x => x > -1);
                int timeoutCount = results.Count() - successResults.Count();

                if (timeoutCount == 4)
                    e.Reply($"Ping {plainText} 超时");
                else
                    e.Reply($"Ping {plainText} 结果：\n延迟：{Math.Round(successResults.Average())} ms\n丢包率：{timeoutCount / 25} %");
            }
            catch (PingException ex)
            {
                e.Reply($"Ping {plainText} 出错，错误原因：{ex.Message}");
            }
        }
    }
}