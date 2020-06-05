using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ.Apps
{
    class RichTextAutoParserApp : App
    {
        public override string Name { get; } = "RichTextAutoParser";
        public override string DisplayName { get; } = "富文本自动解析器";
        public override string Usage { get; } = "发送富文本即可解析（只支持部分）";

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            var richText = parameter[0] as RichText;

            try
            {
                var content = JObject.Parse(richText["content"].Replace(";", "")); // 替换 content 中的 ; 为空，转换为 JObject
                var firstList = content.First.First.ToList(); // 取 content 中第一组数据中的第一组数据，并转换为 List
                var url = firstList.Find(x => ((JProperty)x).Name.ToLower() == "url"); // 在该 List 中寻找 url 参数
                var inaccurateUrl = firstList.Find(x => ((JProperty)x).Name.ToLower().Contains("url")); // 在该 List 中寻找带有 url 参数
                string title = richText["title"] ?? "无标题"; // 定义 title 为 richText 中的 title 参数，如果该 title 参数 null 则为无标题

                if (url != null) // 检测 url 是否为 null
                    e.Source.Send(new Share() { Title = title, Url = new Uri(url.First.ToString()) }); // 发送【分享】CQ 码
                else if (inaccurateUrl != null) // 检测 inaccurateUrl 是否为 null
                    e.Source.Send(new Share() { Title = title, Url = new Uri(inaccurateUrl.First.ToString()) }); // 发送【分享】CQ 码
            }
            catch { }
        }
    }
}
