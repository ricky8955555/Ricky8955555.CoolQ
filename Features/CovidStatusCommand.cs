using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Features
{
    class CovidStatusCommand : Command
    {
        public override string ResponseCommand { get; } = "covid-19";

        protected override string CommandUsage { get; } = "{0}COVID-19";

        static readonly string URL = "https://view.inews.qq.com/g2/getOnsInfo?name=disease_h5";

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

                    e.Source.Send($"数据截至：{dJson["lastUpdateTime"]}\n" +
                        $"累计确诊：{chinaTotal["confirm"]}（较昨日：{chinaAdd["confirm"].ToObject<int>():+#;-#;0}）\n" +
                        $"累计治愈：{chinaTotal["heal"]}（较昨日：{chinaAdd["heal"].ToObject<int>():+#;-#;0}）\n" +
                        $"累计死亡：{chinaTotal["dead"]}（较昨日：{chinaAdd["dead"].ToObject<int>():+#;-#;0}）\n" +
                        $"现有确诊：{chinaTotal["nowConfirm"]}（较昨日：{chinaAdd["nowConfirm"].ToObject<int>():+#;-#;0}）\n" +
                        $"现有疑似：{chinaTotal["suspect"]}（较昨日：{chinaAdd["suspect"].ToObject<int>():+#;-#;0}）\n" +
                        $"现有重症：{chinaTotal["nowSevere"]}（较昨日：{chinaAdd["nowSevere"].ToObject<int>():+#;-#;0}）\n" +
                        "（数据来源：腾讯新闻）");
                }
                catch
                {
                    e.Source.Send($"{e.Sender.At()} 信息处理失败了 (；´д｀)ゞ");
                }
            }
            else
                e.Source.Send($"{e.Sender.At()} 请求失败了 (；´д｀)ゞ");
        }
    }
}
