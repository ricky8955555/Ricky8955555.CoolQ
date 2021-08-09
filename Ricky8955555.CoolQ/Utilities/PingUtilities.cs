using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Ricky8955555.CoolQ
{
    public static class PingUtilities
    {
        private static readonly Ping Ping = new();

        public static PingReply Send(string hostNameOrAddress) => Ping.Send(hostNameOrAddress);

        public static long SendAndGetRoundtripTime(string hostNameOrAddress)
        {
            var result = Send(hostNameOrAddress);

            if (result.Status == IPStatus.Success)
            {
                return result.RoundtripTime;
            }

            if (result.Status == IPStatus.TimedOut)
            {
                return -1;
            }

            throw new PingException(result.Status.ToString());
        }

        public static IEnumerable<long> SendMoreAndGetRoundtripTime(string hostNameOrAddress, int count)
        {
            while (count-- > 0)
            {
                yield return SendAndGetRoundtripTime(hostNameOrAddress);
            }
        }
    }
}