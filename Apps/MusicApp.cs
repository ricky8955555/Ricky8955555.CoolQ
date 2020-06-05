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
    // MusicApp 类 继承 App 类
    class MusicApp : App
    {
        public override string Name { get; } = "Music";
        public override string DisplayName { get; } = "点歌";
        public override string Usage { get; } = "{0}music <歌曲名>";
        public override ParameterRequiredOptions IsParameterRequired { get; } = ParameterRequiredOptions.Necessary;

        static readonly string BaseURL = "http://music.163.com/api/search/pc?s={0}&type=1"; // 定义 BaseURL(API 基础URL)

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter = null)
        {
            string songName = parameter.GetPlainText();
            if (!string.IsNullOrEmpty(songName))
            {
                var musicName = songName.Trim(); // 获取歌曲名，并去除其中前后多余的空格
                var client = new HttpClient(); // 初始化 HttpClient
                var res = client.GetAsync(string.Format(BaseURL, WebUtility.UrlEncode(musicName))).Result; // 发送 Get 请求，并取得结果
                if (res.IsSuccessStatusCode) // 判断是否返回成功
                {
                    var json = JObject.Parse(res.Content.ReadAsStringAsync().Result); // 将返回的 json 信息转换为 JObject 类型
                    try
                    {
                        var musicJson = json["result"]["songs"][0]; // 选中搜索到的第一首歌曲
                        e.Source.Send($"{e.Sender.At()} 这是您点的歌曲哦 φ(>ω<*) ：{string.Join(" / ", musicJson["artists"].Select(x => x["name"]))} - {musicJson["name"]}"); // 发送歌曲信息
                        e.Source.Send(new Music { Id = musicJson["id"].ToObject<int>(), Platform = MusicPlatform.Netease }); // 发送歌曲
                    }
                    catch
                    {
                        e.Source.Send($"{e.Sender.At()} 没有叫 {musicName} 的歌曲哦 (๑＞ڡ＜)☆"); // 提示歌曲不存在
                    }
                }
                else
                    e.Source.Send($"{e.Sender.At()} 请求失败了 (；´д｀)ゞ"); // 提示 HttpClient 请求失败
            }
            else
                NotifyIncorrectUsage(e); // 提示参数错误
        }
    }
}
