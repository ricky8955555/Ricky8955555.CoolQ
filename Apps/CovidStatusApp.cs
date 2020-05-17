using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.QQ;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HuajiTech.QQ.Events;
using Ricky8955555.CoolQ.Tools;

namespace Ricky8955555.CoolQ.Apps
{
    class CovidStatusApp : App
    {
        public override string Name { get; } = "CovidStatus";
        public override string DisplayName { get; } = "疫情动态";
        public override string Command { get; } = "covid-19";
        public override string Usage { get; } = "COVID-19";
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Unnecessary;

        static readonly string URL = "https://view.inews.qq.com/g2/getOnsInfo?name=disease_h5";
        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter)
        {
            var (isSuccessful, result) = HttpGetTool.GetAndCheckIsSuccessful(URL); // 发送 Get 请求，并取得结果
            if (isSuccessful) // 判断是否返回成功
            {
                try
                {
                    var json = JObject.Parse(result); // 将返回的 json 信息转换为 JObject 类型
                    var dJson = JObject.Parse(json["data"].ToString()); // 将返回的 json 中 data 信息转换为 JObject 类型
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
                    e.Source.Send($"{e.Sender.At()} 信息处理失败了 (；´д｀)ゞ"); // 提示 Json 处理失败
                }
            }
            else
                e.Source.Send($"{e.Sender.At()} 请求失败了 (；´д｀)ゞ"); // 提示 HttpClient 请求失败
        }
    }
}
