using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ
{
    internal static class PingUtilities
    {
        internal static PingReply Send(string hostNameOrAddress) => new Ping().Send(hostNameOrAddress);

        internal static long SendAndGetRoundtripTime(string hostNameOrAddress)
        {

            var result = Send(hostNameOrAddress);

            if (result.Status == IPStatus.Success)
                return result.RoundtripTime;
            else if (result.Status == IPStatus.TimedOut)
                return -1;
            else
                throw new PingException(result.Status.ToString());
        }

        internal static IEnumerable<long> SendMoreAndGetRoundtripTime(string hostNameOrAddress, int count)
        {
            for (int i = 0; i < count; i++)
                yield return SendAndGetRoundtripTime(hostNameOrAddress);

            yield break;
        }
    }
}