using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ricky8955555.CoolQ.Features
{
    internal class WeatherCommand : Command<PlainText>
    {
        protected override string CommandUsage { get; } = "{0}weather <城市>";

        internal override string ResponseCommand { get; } = "weather";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText)
        {
            e.Reply(Resources.Processing);

            if (HttpUtilities.HttpGet(string.Format(Resources.WeatherApiURL, plainText), out string content))
            {
                try
                {
                    var json = JObject.Parse(content);
                    int errorCode = json["error"].ToObject<int>();

                    if (errorCode == 0)
                    {
                        var result = json["results"][0];
                        var weather = result["weather_data"];
                        string nowTemperature = Regex.Match(weather[0]["date"].ToString(), @"（实时：|\d{2}℃|）").Value;
                        var weatherStrs = weather.Select(x => $"{x["date"].ToString().Substring(0, 2)}：{x["weather"]} {GetTemperature(x["temperature"].ToString())} {x["wind"]}");

                        e.Source.Send($"城市 {result["currentCity"]} 天气：\n实时：{nowTemperature} PM2.5：{result["pm25"]} μg/m³\n{string.Join("\n", weatherStrs)}");
                    }
                    else if (errorCode == -3)
                        e.Reply($"城市 {plainText} 不存在 (๑＞ڡ＜)☆");
                    else
                        e.Reply($"请求出错了，错误码：{json["error"]}，错误原因：{json["status"]}");
                }
                catch
                {
                    e.Reply($"信息处理失败了 (；´д｀)ゞ");
                }
            }
            else
                e.Reply($"请求失败了 (；´д｀)ゞ");
        }

        private static string GetTemperature(string str) => string.Join(" ~ ", str.Substring(0, str.Length - 1).Split(new string[] { " ~ " }, StringSplitOptions.None).Reverse()) + "℃";
    }
}