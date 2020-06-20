using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using System.Net.NetworkInformation;
using static Ricky8955555.CoolQ.FeatureResources.PingResources;

namespace Ricky8955555.CoolQ.Features
{
    class PingCommand : Command<PlainText>
    {
        public override string ResponseCommand { get; } = "ping";

        protected override string CommandUsage { get; } = "{0}ping <IP 地址或域名>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            var ping = new Ping();
            long totalRoundtripTime = 0;
            int packetLostCount = 0;
            string[] splitText = plainText.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int pingCount = 4;

            if (splitText != null &&
                splitText.Length == 2 && int.TryParse(splitText[1], out pingCount) && pingCount > 0 ||
                splitText.Length == 1)
            {
                string address = splitText[0];

                try
                {

                    for (int i = 0; i < pingCount; i++)
                    {
                        var pingReply = ping.Send(address);
                        if (pingReply.Status == IPStatus.Success)
                            totalRoundtripTime += pingReply.RoundtripTime;
                        else if (pingReply.Status == IPStatus.TimedOut)
                            packetLostCount += 1;
                        else
                        {
                            e.Source.Send(string.Format(Failed, address, pingReply.Status.ToString()));
                            return;
                        }
                    }

                    if (packetLostCount < pingCount)
                        e.Source.Send(string.Format(Success, address, totalRoundtripTime / pingCount, packetLostCount / pingCount * 100));
                    else
                        e.Source.Send(string.Format(TimedOut, address));
                }
                catch (Exception ex)
                {
                    e.Source.Send(string.Format(Error, address, ex.Message));
                }
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
