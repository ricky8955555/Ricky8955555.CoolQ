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

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            var richText = parameter[0] as RichText;

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
