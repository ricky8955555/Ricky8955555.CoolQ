using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using static Ricky8955555.CoolQ.FeatureResources.MusicResources;

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
                var res = client.GetAsync(string.Format(BaseURL, WebUtility.UrlEncode(musicName))).Result;
                if (res.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                    try
                    {
                        var musicJson = json["result"]["songs"][0];
                        e.Reply(string.Format(Notification, string.Join(" / ", musicJson["artists"].Select(x => x["name"])), musicJson["name"]));
                        e.Source.Send(new Music { Id = musicJson["id"].ToObject<int>(), Platform = MusicPlatform.Netease });
                    }
                    catch
                    {
                        e.Reply(string.Format(DoesNotExist, songName));
                    }
                }
                else
                    e.Reply(NoResponse);
            }
            else
                NotifyIncorrectUsage(e);
        }
    }
}
