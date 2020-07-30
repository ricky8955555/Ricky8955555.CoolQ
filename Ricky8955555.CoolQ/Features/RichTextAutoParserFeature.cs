using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Ricky8955555.CoolQ.Features
{
    internal class RichTextAutoParserFeature : Feature
    {
        internal override string Usage { get; } = "发送富文本即可解析（只支持部分）";

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            var elements = e.Message.Parse();
            var richText = elements[0] as RichText;

            if (richText != null)
                try
                {
                    var content = JObject.Parse(richText["content"].Replace(";", ""));
                    var firstList = content.First.First.ToList();
                    var url = firstList.Find(x => ((JProperty)x).Name.ToLower() == "url");
                    var inaccurateUrl = firstList.Find(x => ((JProperty)x).Name.ToLower().Contains("url"));
                    string title = richText["title"] ?? "无标题";

                    if (url != null)
                        e.Source.Send(new Share() { Title = title, Url = new Uri(url.First.ToString()) });
                    else if (inaccurateUrl != null)
                        e.Source.Send(new Share() { Title = title, Url = new Uri(inaccurateUrl.First.ToString()) });
                }
                catch { }
        }
    }
}