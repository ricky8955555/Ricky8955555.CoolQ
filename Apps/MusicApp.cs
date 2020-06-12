using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Ricky8955555.CoolQ.Apps
{
    class MusicApp : App
    {
        public override string Name { get; } = "Music";
        public override string DisplayName { get; } = "点歌";
        public override string Usage { get; } = "{0}music <歌曲名>";

        static readonly string BaseURL = "http://music.163.com/api/search/pc?s={0}&type=1";

        protected override void Invoke(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string songName = parameter.GetPlainText();

            if (parameter?[0] is PlainText && !string.IsNullOrEmpty(songName))
            {
                var musicName = songName.Trim();
                var client = new HttpClient();
                var res = client.GetAsync(string.Format(BaseURL, WebUtility.UrlEncode(musicName))).Result;
                if (res.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                    try
                    {
                        var musicJson = json["result"]["songs"][0];
                        e.Source.Send($"{e.Sender.At()} 这是您点的歌曲哦 φ(>ω<*) ：{string.Join(" / ", musicJson["artists"].Select(x => x["name"]))} - {musicJson["name"]}");
                        e.Source.Send(new Music { Id = musicJson["id"].ToObject<int>(), Platform = MusicPlatform.Netease });
                    }
                    catch
                    {
                        e.Source.Send($"{e.Sender.At()} 没有叫 {musicName} 的歌曲哦 (๑＞ڡ＜)☆");
                    }
                }
                else
                    e.Source.Send($"{e.Sender.At()} 请求失败了 (；´д｀)ゞ");
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
