# XCY UI函数式组合跨平台UI引擎软件
这是一款基于 C# + SkiaSharp 构建的跨平台声明式 UI 框架，深度借鉴 Jetpack Compose 现代化设计理念，彻底摒弃 WPF、Avalonia 传统的 XAML + MVVM 臃肿架构，全新采用简洁高效的函数组合式 UI 开发模型。让 .NET 也能像移动端、前端一样拥有现代化的极速开发体验。

## ✨ 项目介绍
- 项目采用插拔式构架, XcyUI.GLFW(实现IWindow适配window),XcyUI(核心层),XcyUI.SkiaSharp(实现IDraw，适配渲染),可以实现不同的窗口适配和渲染适配，可以很好的扩展到web端以及移动端
- 函数组合式 API + 状态对象驱动界面重组
- 自研 **无Timer高性能动画系统**
- 完整UI布局系统：Row / Column / Flow / 虚拟滚动
- 百万级数据列表轻松稳定 **60fps+**
- 自研渲染管线 + **脏矩形局部刷新**
- 支持 **.NET8 AOT 原生发布**
- 已验证平台：Windows 10+ / Ubuntu22.04 /macOS m4
- 支持热重载、自适应频率分辨率


## 代码示例
```csharp
using Demo.Theme;
using XchyUIDemo;
using XcyUI.models;
using XcyUI.SkiaSharp;
using static XcyUI.GLFW.window.XWindowWidget;
using static XcyUI.widgets.XWidget;
using XcyUI.widgets.extensions;

MainWindow(() =>
{
    Column(() =>
    {
        var countState = StateValueOf(0);
        Text().Bind(countState, (builder, count) =>
        {
            builder.Content("简单计数器:" + count);
        }, needLayout: true);
        Text("Click").Button().Click(() => countState.Value += 10);
    })
    .Space(10)
    .VerticalAlignment(XVerticalAlignment.Center);
})
.Title("Xcy UI demo")
.Size(1500, 900)
.RenderBackend(new SkiaRenderBackend())
.OnLoad(HotkeyManager.Start)
.Show();
```
只需要创建控制台程序就可以开发桌面软件，没有额外的配置以及编译要求，不需要项目模板，创建一个window只需要一个MainWindow()函数就可以，UI界面都是函数

项目是本人一人开发，很定有很多不足bug,有问题希望帮忙提issue


该UI引擎具体具体如何使用加微信号 ***xiaochangyanwx*** 入群分享
