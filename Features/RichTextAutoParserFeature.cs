using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Features
{
    class RichTextAutoParserFeature : Feature
    {
        public static List<Group> Groups = new List<Group>();

        public override void Invoke(MessageReceivedEventArgs e)
        {
            if (e.Source is Group group && Groups.Contains(group) &&
                e.Message.Parse().TryDeconstruct(out RichText richText) &&
                richText["content"] != null)
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
                        e.Source.Send(new Share() { Title = title, Url = new Uri(inaccurateUrl.First.ToString()) } );
                }
                catch { }
        }

        public static bool Switch(Group group)
        {
            var isExist = Groups.Contains(group);
            if (isExist)
                Groups.Remove(group);
            else
                Groups.Add(group);

            return isExist;
        }
    }
}