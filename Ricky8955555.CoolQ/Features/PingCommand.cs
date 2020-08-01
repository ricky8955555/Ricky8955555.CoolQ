using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Ricky8955555.CoolQ.Features
{
    internal class PingCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "ping";

        protected override string CommandUsage { get; } = "{0}ping <IP 地址或域名> (Ping 4 次取平均值)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, ComplexMessage elements)
        {
            e.Reply(Resources.Processing);

            try
            {
                var results = PingUtilities.SendMoreAndGetRoundtripTime(plainText, 4);
                var successResults = results.Where(x => x > -1);
                int timedoutCount = results.Count() - successResults.Count();

                if (timedoutCount == 4)
                    e.Reply($"Ping {plainText} 超时");
                else
                    e.Reply($"Ping {plainText} 结果：\n延迟：{Math.Round(successResults.Average())} ms\n丢包率：{timedoutCount / 25} %");
            }
            catch (PingException ex)
            {
                e.Reply($"Ping {plainText} 出错，错误原因：{ex.Message}");
            }
        }
    }
}