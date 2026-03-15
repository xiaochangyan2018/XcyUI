# XchyUI函数式组合跨平台UI引擎软件
内核 <200KB · .NET8 AOT跨平台 · 百万数据60fps+ · 无Web套壳

## ✨ 项目介绍
这是一个函数式组合编程的GUI框架，参考android compose写法做的GUI在.net端实现,写法和传统的GUI有很大区别，熟悉flutter、react、jetpack copmose的可以快速上手，否则需要先理解函数组合编程的理念。

框架设计追求极简与高效：
• 单线程架构 + 对象复用机制，大幅降低GC压力
• 元素结构无冗余设计，内存占用极低
• 函数式组合编程 + 状态驱动界面重组
• 组件树一次声明、多处复用
• 业务逻辑与UI结构高度内聚，不分散
• 思想贴近 React / Flutter / Jetpack Compose，现代前端/移动端开发者可快速上手

与传统XML、重量级框架不同，本引擎坚持 **小而精** 的设计理念：
只提供最基础的原子组件，所有复杂组件（DataGrid、TreeView、图表、卡片等）均通过基础组件**积木式组合**实现。
框架不提供冗余、不内置臃肿组件，保持最轻量、最灵活、最可定制的核心优势。

全程无黑盒、无深度封装、无Web套壳、无浏览器内核，回归原生渲染本质。


## 🎯 核心特性
- 纯C#用户态实现，**Release 内核 DLL < 200KB**
- 函数组合式 API + 状态对象驱动界面重组
- 自研 **无Timer高性能动画系统**
- 完整布局系统：Row / Column / Flow / 虚拟滚动
- 百万级数据列表轻松稳定 **60fps+**
- 自研渲染管线 + **脏矩形局部刷新**
- 窗口对接：Silk.NET.GLFW(可以定制对接其他窗口库)
- 渲染引擎：SkiaSharp(可以定制对接其他渲染库如NanoVG/Direct2D）
- 支持 **.NET8 AOT 原生发布**
- 已验证平台：Windows 10+ / Ubuntu22.04 /macOS m4
- 支持热重载、自适应频率分辨率


## 📌 极简示例代码
```csharp
ContentView(() => {
    Column(() => {
        // 响应式状态
        var counterNum = StateValueOf(0);
        Text()
           .H3() //内置基础样式
           .Binding(counterNum, (builder, num) => {
               builder.TextValue($"计数器：{num}");
           }, needLayout: true); //改变文本需要重新布局，默认为false

        // 无Timer循环动画
        var visibleState = StateValueOf(true);
        var animateValue = AnimateFloatOf(visibleState, animate => {
               animate.Duration = 800;
               animate.Times = int.MaxValue;
               animate.Delay = 200;
               animate.Interpolator = XAnimationInterpolator.Uniform;
           });

        Icon(SvgResources.CircleProgress)
           .Size(32)
           .Binding(animateValue, (builder, value) =>
               builder.Rotate(value * 360)
           );

        // 点击交互
        Text("点击增加计数")
           .PrimaryButton()
           .Click(() => counterNum.Value++);
    })
   .Size(WRAP)
   .Space(10);
});
```

更多实例用法可以参XchyUI.Componets里面的一些组件的实现，这个组件库会陆续添加一些非常常用组件的默认实现、以及一些需要自绘或者特殊算法的组件、一些常用图表、大家也可以模仿着实现自己的组件。

### 可以下载下来直接visual stuido 2022及以上打开，直接运行, 项目会一直优化更新

### 热重载使用：
框架并没有提供完整的热重载功能，而是只提供刷新界面的方法，热重载的能力是用的visual studio自己的能力

下面是界面全部刷新的方法
```C#
XWidget.HotReload.Send(true);
RenderImp.InvalidateAll();
```
demo里面加了一个 HotkeyManager用来监听ctrl+s来出发alt+10 触发热重载功能，然后再调用上面的方法刷新界面，同时也需要在.csproj文件里面添加HotReloadEnabled为true

```C#
<HotReloadEnabled>true</HotReloadEnabled>
```

### 关于win7系统的支持
XchyUIDemo.SharpShap2x 这个是对接的SkiaSharp2.88.6 因为本人没有win7电脑所以没测，大家如果需要win7支持可以试试这个版本


该UI引擎具体具体如何使用加微信号 ***xiaochangyanwx*** 入群分享
