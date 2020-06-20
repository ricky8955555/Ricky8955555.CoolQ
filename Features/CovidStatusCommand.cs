using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using static Ricky8955555.CoolQ.FeatureResources.CovidStatusResources;

namespace Ricky8955555.CoolQ.Features
{
    class CovidStatusCommand : Command
    {
        public override string ResponseCommand { get; } = "covid-19";

        protected override string CommandUsage { get; } = "{0}COVID-19";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            var client = new HttpClient();
            var res = client.GetAsync(URL).Result;
            if (res.IsSuccessStatusCode)
            {
                try
                {
                    var json = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                    var dJson = JObject.Parse(json["data"].ToString());
                    var chinaTotal = dJson["chinaTotal"];
                    var chinaAdd = dJson["chinaAdd"];

                    e.Source.Send(string.Format(DataUpdatingTime, dJson["lastUpdateTime"]) + "\n" +
                        string.Format(Confirmed, chinaTotal["confirm"]) + string.Format(CompareToYesterday, chinaAdd["confirm"]) + "\n" +
                        string.Format(Healed, chinaTotal["heal"]) + string.Format(CompareToYesterday, chinaAdd["heal"]) + "\n" +
                        string.Format(Dead, chinaTotal["dead"]) + string.Format(CompareToYesterday, chinaAdd["dead"]) + "\n" +
                        string.Format(NowConfirmed, chinaTotal["nowConfirm"]) + string.Format(CompareToYesterday, chinaAdd["nowConfirm"]) + "\n" +
                        string.Format(NowSuspected, chinaTotal["suspect"]) + string.Format(CompareToYesterday, chinaAdd["suspect"]) + "\n" +
                        string.Format(NowSevere, chinaTotal["nowSevere"]) + string.Format(CompareToYesterday, chinaAdd["nowSevere"]) + "\n");
                }
                catch
                {
                    e.Reply(IncorrectInfo);
                }
            }
            else
                e.Reply(NoResponse);
        }
    }
}
