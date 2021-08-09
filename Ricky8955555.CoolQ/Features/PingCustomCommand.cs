using System.Linq;
using System.Net.NetworkInformation;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ.Features
{
    public class PingCustomCommand : Command<PlainText, PlainText>
    {
        public override string ResponseCommand { get; } = "ping";

        protected override string CommandUsage { get; } = "{0}ping <IP 地址或域名> <次数>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, PlainText countText, ComplexMessage elements)
        {
            if (int.TryParse(countText, out int count) && count < 100)
            {
                e.Reply(Resources.Processing);

                try
                {
                    var results = PingUtilities.SendMoreAndGetRoundtripTime(plainText, count);
                    var successResults = results.Where(x => x > -1);
                    int timeoutCount = results.Count() - successResults.Count();

                    if (timeoutCount == count)
                        e.Reply($"Ping {plainText} 超时");
                    else
                        e.Reply($"Ping {plainText} 结果：\n延迟：{string.Join(", ", successResults)} ms\n丢包率：{timeoutCount / count * 100} %");
                }
                catch (PingException ex)
                {
                    e.Reply($"Ping {plainText} 出错，错误原因：{ex.Message}");
                }
            }
        }
    }
}
