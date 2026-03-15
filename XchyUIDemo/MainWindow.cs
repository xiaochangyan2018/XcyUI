using XchyUI.Components;
using XchyUI.GLFW.window;
using XchyUI.navigation;
using XchyUI.widgets;
using XchyUI.widgets.extensions;
using static XchyUI.widgets.XWidget;
using static XchyUI.Components.Compoments;
using System.Diagnostics;

namespace XchyUIDemo
{
    public class MainWindow: XWindow
    {
        public MainWindow()
        {
            Width = 1380;
            Height = 800;
            Title = "XchyUI Demo";
        }

        public override void OnLoad()
        {
            var page = new XPage();            
            page.RootView = ContentView(() =>
            {
                Column(() =>
                {
                    var visiblePopover = StateValueOf(false);
                    var dateTime = DateTime.Now;
                    var dateTimeState = StateValueOf(dateTime);
                    Input().PrimaryInput().Width(300)
                    .Binding(dateTimeState, (builder, date) =>
                    {
                        builder.TextValue(date.ToString());
                    }, needLayout: true)
                    .Popover(visiblePopover, () =>
                    {
                        var stopWatch = new Stopwatch();
                        stopWatch.Start();
                        DateTimePicker(dateTimeState.Value, date =>
                        {
                            Console.WriteLine(date);
                            dateTimeState.Value = date;
                            visiblePopover.Value = false;
                        }).Margin(10);
                        stopWatch.Stop();
                        Console.WriteLine("show....." + stopWatch.ElapsedMilliseconds);
                    });
                    // 响应式状态
                    var counterNum = StateValueOf(0);
                    Text()
                       .H3() //内置基础样式
                       .Binding(counterNum, (builder, num) =>
                       {
                           builder.TextValue($"一个简单的计数器：{num}");
                       }, needLayout: true); //改变文本需要重新布局，默认为false


                    Icon(SvgRes.Loading)
                        .Color(xTheme.Colors.PrimaryText)
                        .Size(32).CircleProgress();
                    // 点击交互
                    Text("点击增加计数")
                       .PrimaryButton()
                       .Click(() =>
                       {
                           counterNum.Value++;
                       });

                    var loadingState = StateValueOf(false);
                    IconButton(SvgRes.Search, "Search",loadingState: loadingState)
                    .Click(() =>
                    {
                        loadingState.Value = !loadingState.Value;
                    });
                })
                 .Size(WRAP)
                 .Padding(10)
                 .Space(10);
            }).View;
            OpenPage(page);
        }
    }
}
