using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Ricky8955555.CoolQ.Features
{
    class MusicCommand : Command
    {
        public override string ResponseCommand { get; } = "music";

        protected override string CommandUsage { get; } = "{0}music <歌曲名>";

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string songName = parameter.GetPlainText();

            if (parameter?[0] is PlainText && !string.IsNullOrEmpty(songName))
            {
                var musicName = songName.Trim();
                var client = new HttpClient();
                var res = client.GetAsync(string.Format(Resources.MusicApiURL, WebUtility.UrlEncode(musicName))).Result;
                if (res.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                    try
                    {
                        var musicJson = json["result"]["songs"][0];
                        e.Reply($"这是您点的歌曲哦 φ(>ω<*) ：{string.Join(" / ", musicJson["artists"].Select(x => x["name"]))} - {musicJson["name"]}");
                        e.Source.Send(new Music { Id = musicJson["id"].ToObject<int>(), Platform = MusicPlatform.Netease });
                    }
                    catch
                    {
                        e.Reply($"没有叫 {musicName} 的歌曲哦 (๑＞ڡ＜)☆");
                    }
                }
                else
                    e.Reply($"请求失败了 (；´д｀)ゞ");
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
