﻿using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ.Features
{
    public class SlappingFeature : Feature
    {
        public override string Usage { get; } = "当发送者单条消息中含有 2 个或以上对同个人提及时，则发送 “@发送者 拍了拍 @提及者 {自定义语句}”";

        public override void Invoke(MessageReceivedEventArgs e)
        {
            var message = e.Message.Parse();
            var mentions = message.OfType<Mention>();
            var distinctMentions = mentions.Distinct();

            if (distinctMentions.Count() == 1)
            {
                var mention = distinctMentions.Single();
                var target = mention.GetTarget();
                var plainTexts = message.OfType<PlainText>();
                var others = message.Except(mentions).Except(plainTexts);

                if (!others.Any() && string.IsNullOrWhiteSpace(string.Join("", plainTexts)) && message.Where(mention.Equals).Count() > 1)
                {
                    string custom = string.Empty;
                    var config = (JObject)SlappingConfig.Config;
                    string numStr = target.Number.ToString();

                    if (config.ContainsKey(numStr))
                        custom = " " + config[numStr].ToString();

                    if (target.Equals(e.Subject))
                        e.Reply("拍了拍 自己" + custom);
                    else
                        e.Reply("拍了拍 " + mention + custom);
                }
            }
        }
    }
}