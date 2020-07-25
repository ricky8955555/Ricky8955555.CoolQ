using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class IPInfoCommand : Command<PlainText>
    {
        internal override string ResponseCommand { get; } = "ipinfo";

        protected override string CommandUsage { get; } = "{0}ipinfo <IP 地址或域名>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            e.Reply(Resources.Processing);

            if (HttpUtilities.HttpGet(string.Format(Resources.IPInfoApiURL, plainText), out string content))
            {
                try
                {
                    var json = JObject.Parse(content);
                    if (json["status"].ToString() == "success")
                    {
                        e.Source.Send($"状态：获取成功\n" +
                                              $"IP {json["query"]} 信息：\n" +
                                              $"洲：{json["continent"]}（{json["continentCode"]}）\n" +
                                              $"国家：{json["country"]}（{json["countryCode"]}）\n" +
                                              $"地区：{json["regionName"]}（{json["region"]}）\n" +
                                              $"城市：{json["city"]}\n" +
                                              $"邮政编码：{json["zip"]}\n" +
                                              $"经纬度：{json["lat"]}, {json["lon"]}\n" +
                                              $"时区：{json["timezone"]}\n" +
                                              $"货币单位：{json["currency"]}\n" +
                                              $"ISP：{json["isp"]}\n" +
                                              $"所属组织：{json["org"]}\n" +
                                              $"AS：{json["asname"]}（{json["as"]}）\n" +
                                              $"反向DNS：{json["reverse"]}\n" +
                                              $"移动数据接入：{(json["mobile"].ToObject<bool>() ? "是" : "否")}\n" +
                                              $"代理：{(json["proxy"].ToObject<bool>() ? "是" : "否")}\n" +
                                              $"托管/数据中心：{(json["hosting"].ToObject<bool>() ? "是" : "否")}");
                    }
                    else
                        e.Reply("获取 IP 信息失败" + (json.ContainsKey("message") ? "，错误原因：" + json["message"] : string.Empty));
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