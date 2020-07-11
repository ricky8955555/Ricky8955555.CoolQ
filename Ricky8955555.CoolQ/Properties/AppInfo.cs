// 用于 HuajiTech.CoolQ 的应用信息配置文件。
// 最后更新于 2020-7-3 19:44。

using HuajiTech.CoolQ;
using Ricky8955555.CoolQ;

// 指定资源管理器的默认区域性信息。通常情况下，无需更改此项。
[assembly: System.Resources.NeutralResourcesLanguage("zh-CN")]

// 指定应用的初始化器。
// 应用初始化器必须包含一个名为 Init 的无参数静态方法。
// 注意：必须使用字符串指定初始化器。
[assembly: Initializer("Ricky8955555.CoolQ.Initializer")]

// 指定应用的 AppID。必须更改此项。
// dev 目录下存放应用的目录名必须与 AppID 匹配。
[assembly: AppId(Commons.AppId)]