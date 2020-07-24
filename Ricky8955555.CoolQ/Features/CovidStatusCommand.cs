using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Features
{
    class CovidStatusCommand : Command
    {
        internal override string ResponseCommand { get; } = "covid-19";

        protected override string CommandUsage { get; } = "{0}COVID-19";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null)
        {
            e.Reply(Resources.Processing);

            if (HttpUtilities.HttpGet(Resources.CovidStatusApiURL, out string content))
            {
                try
                {
                    var json = JObject.Parse(content);
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
                    e.Reply($"信息处理失败了 (；´д｀)ゞ");
                }
            }
            else
                e.Reply($"请求失败了 (；´д｀)ゞ");
        }
    }
}