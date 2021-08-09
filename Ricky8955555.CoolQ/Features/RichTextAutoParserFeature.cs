using System;
using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Features
{
    public class RichTextAutoParserFeature : Feature
    {
        public override string Usage { get; } = "发送富文本即可解析（只支持部分）";

        public override void Invoke(MessageReceivedEventArgs e)
        {
            var elements = e.Message.Parse();

            if (elements[0] is RichText richText)
                try
                {
                    var content = JObject.Parse(richText["content"].Replace(";", ""));
                    var firstList = content.First.First.ToArray();
                    var url = firstList.FirstOrDefault(x => ((JProperty)x).Name.Equals("url", StringComparison.OrdinalIgnoreCase));
                    var inaccurateUrl = firstList.FirstOrDefault(x => ((JProperty)x).Name.Contains("url", StringComparison.OrdinalIgnoreCase));
                    string title = richText["title"] ?? "无标题";

                    if (url is not null)
                        e.Source.Send(new Share() { Title = title, Url = new Uri(url.First.ToString()) });
                    else if (inaccurateUrl is not null)
                        e.Source.Send(new Share() { Title = title, Url = new Uri(inaccurateUrl.First.ToString()) });
                }
                catch { }
        }
    }
}