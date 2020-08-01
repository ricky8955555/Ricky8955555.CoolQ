using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;

namespace Ricky8955555.CoolQ.Features
{
    internal class MusicCommand : Command
    {
        internal override string ResponseCommand { get; } = "music";

        protected override string CommandUsage { get; } = "{0}music <歌曲名>";

        protected override LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.ComplexMessage;

        protected override void Invoking(MessageReceivedEventArgs e, ComplexMessage elements)
        {
            var plainText = elements.OfType<PlainText>();

            if (plainText.Count() == elements.Count)
            {
                e.Reply(Resources.Processing);

                string str = string.Join(" ", plainText);
                string musicName = str.Trim();

                if (HttpUtilities.HttpGet(string.Format(Resources.MusicApiURL, WebUtility.UrlEncode(musicName)), out string content))
                {
                    try
                    {
                        var json = JObject.Parse(content);
                        var musicJson = json["result"]["songs"][0];
                        e.Reply($"这是您点的歌曲哦 φ(>ω<*) ：{string.Join(" / ", musicJson["artists"].Select(x => x["name"]))} - {musicJson["name"]}");
                        e.Source.Send(new Music { Id = musicJson["id"].ToObject<int>(), Platform = MusicPlatform.Netease });
                    }
                    catch (ApiException ex) when (ex.ErrorCode == -11)
                    {
                        e.Reply("歌曲发送失败了 (；´д｀)ゞ");
                    }
                    catch (ApiException)
                    {
                        throw;
                    }
                    catch
                    {
                        e.Reply($"没有叫 {musicName} 的歌曲哦 (๑＞ڡ＜)☆");
                    }
                }
                else
                    e.Reply($"请求失败了 (；´д｀)ゞ");
            }
        }
    }
}