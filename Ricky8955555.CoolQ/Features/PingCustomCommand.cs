using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Ricky8955555.CoolQ.Features
{
    internal class PingCustomCommand : Command<PlainText, PlainText>
    {
        internal override string ResponseCommand { get; } = "ping";

        protected override string CommandUsage { get; } = "{0}ping <IP 地址或域名> <次数> (Ping 取平均值)";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, PlainText countText, ComplexMessage elements)
        {
            if (int.TryParse(countText, out int count) && count < 100)
            {
                e.Reply(Resources.Processing);

                try
                {
                    var results = PingUtilities.SendMoreAndGetRoundtripTime(plainText, count);
                    var successResults = results.Where(x => x > -1);
                    int timedoutCount = results.Count() - successResults.Count();

                    if (timedoutCount == count)
                        e.Reply($"Ping {plainText} 超时");
                    else
                        e.Reply($"Ping {plainText} 结果：\n延迟：{Math.Round(successResults.Average())} ms\n丢包率：{timedoutCount / count * 100} %");
                }
                catch (PingException ex)
                {
                    e.Reply($"Ping {plainText} 出错，错误原因：{ex.Message}");
                }
            }
        }
    }
}
