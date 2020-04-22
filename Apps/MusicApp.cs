﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Messaging;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Apps
{
    // MusicApp 类 继承 App 类
    class MusicApp : App
    {
        public override string Name { get; } = "Music";
        public override string DisplayName { get; } = "点歌";
        public override string Command { get; } = "music";
        public override string Usage { get; } = "music <歌曲名>";
        public override bool IsParameterRequired { get; } = true;

        static readonly string BaseURL = "http://192.168.0.233:3001"; // 定义 BaseURL(API 基础URL)

        public override void Run(MessageReceivedEventArgs e, ComplexMessage parameter)
        {
            if (parameter.TryDeconstruct(out PlainText songName))
            {
                var client = new HttpClient(); // 初始化 HttpClient
                var musicName = songName.Content.Trim(); // 获取歌曲名，并去除其中前后多余的空格
                var res = client.GetAsync($"{BaseURL}/search?keywords={WebUtility.UrlEncode(musicName)}").Result; // 发送 Get 请求，并取得结果
                if (res.IsSuccessStatusCode) // 判断是否返回成功
                {
                    var json = JObject.Parse(res.Content.ReadAsStringAsync().Result); // 将返回的 json 信息转换为 JObject 类型
                    try
                    {
                        var musicJson = json["result"]["songs"][0]; // 选中搜索到的第一首歌曲
                        e.Source.Send($"{e.Sender.At()} 这是您点的歌曲哦 φ(>ω<*) ：{string.Join(" / ", musicJson["artists"].Select(x => x["name"]))} - {musicJson["name"]}"); // 发送歌曲信息
                        e.Source.Send(new Music { Id = musicJson["id"].ToObject<int>(), Provider = MusicProvider.Netease }); // 发送歌曲
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
                e.Source.Send($"{e.Sender.At()} 参数错误 (￣３￣)a ，具体用法：{Usage}"); // 提示参数错误
        }
    }
}