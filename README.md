# Ricky8955555.CoolQ

![GitHub](https://img.shields.io/github/license/ricky8955555/Ricky8955555.CoolQ)
![GitHub last commit](https://img.shields.io/github/last-commit/ricky8955555/Ricky8955555.CoolQ)
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/ricky8955555/Ricky8955555.CoolQ)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/ricky8955555/Ricky8955555.CoolQ)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fricky8955555%2FRicky8955555.CoolQ.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Fricky8955555%2FRicky8955555.CoolQ?ref=badge_shield)
[![Build status](https://ci.appveyor.com/api/projects/status/4fev7v95w57jbh4c?svg=true)](https://ci.appveyor.com/project/ricky8955555/ricky8955555-coolq)

一款基于 酷Q 的机器人插件

## 简介

基于 CoolQ (智能机器人软件) & HuajiTech.CoolQ (SDK) 开发的一款插件

感谢 CoolQ 提供软件支持 和 SYC 提供 SDK 支持

### 链接：

- HuajiTech.CoolQ (SDK) 项目 (GitHub): https://github.com/huajitech/coolq-dotnet-sdk
- 酷Q 官网: https://cqp.cc
- 最新 Build 下载：http://ricky8955555.byethost4.com/app.zip

## 应用

- Ping
- 帮助菜单
- 点歌
- 富文本自动解析器
- 黑名单管理器【主人】
- 空格化（字符串中添加空格）
- 疫情动态
- 应用开关【管理员】
- 自动复读
- 假Mention解析器
- 拍一拍
- 营销号文章生成器
- 关于本插件
- 配置

## 使用方法

1. 使用时，先启用插件，使插件初始化

2. 在 酷Q 程序文件夹下，打开 data\app\io.ricky8955555.github.addons\PluginConfig.json 配置插件：

    - Owner -> 机器人主人
    - Prefix -> 指令前缀（支持 CQ 码）

示例：

```json
{
  "Prefix": "/",
  "Owner": 397050061
}
```

3. 修改完毕后，重载应用

4. 发送 “前缀 + help” 指令即可获取帮助菜单