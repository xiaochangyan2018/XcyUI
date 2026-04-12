using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XchyUI.Components;
using XchyUI.expansions;
using XchyUI.models;
using XchyUI.utils;
using XchyUI.widgets;
using XchyUI.widgets.extensions;
using static XchyUI.widgets.XWidget;

namespace XchyUI.Demo.elmedemo
{
    public class MenuItem
    {
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public bool IsSelected { get; set; }
    }
    public static class LeftMneu
    {
        private static List<MenuItem> menus = new();
        private static XState<List<MenuItem>> menusState = new();
        private static void LoadMenus()
        {
            if(menus.Count == 0)
            {
                var datas = new List<string>()
                    {
                        "组件总览,1",
                        "组件总览,0",
                        "Basic 基础组件,1",
                        "Text 文本,0",
                        "Button 按钮,0",
                        "IconButton 图标按钮,0",
                        "Border 边框,0",
                        "Box 堆叠容器,0",
                        "Column 纵向容器,0",
                        "Row 横向容器,0",
                        "Flow 弹性容器,0",
                        "Spacer 空白占位,0",
                        "Form 表单组件,1",
                        "Input 文本框,0",
                        "InputWithIcon 图标文本框,0",
                        "Radio/checkbox 单选框/多选框,0",
                        "Select 选择框,0",
                        "Slider 滑块,0",
                        "Switch 开关,0",
                        "数据表格,1",
                        "TreeView 树组件,0",
                        "TreeGrid 树表格,0",
                        "DataGrid 数据表格,0",
                        "PopupCard 弹出框,1",
                        "Popover 弹出框,0",
                        "Dialog 对话框,0",
                        "Toolip 鼠标悬浮提示,0",
                        "Toast 消息提示,0",
                        "DialogForm 对话框表单,0",
                        "图表,1",
                        "LineChart 折线图,0",
                        "BarChart 柱形图,0",
                        "Scatter  散点图,0",
                        "PieChart 饼图,0"
                    };
                var menus = new List<MenuItem>();
                foreach (var item in datas)
                {
                    var name = item.Split(",");
                    var menu = new MenuItem()
                    {
                        Name = name[0],
                        IsGroup = name[1] == "1"
                    };
                    menus.Add(menu);
                }
                menusState.Value = menus;
            }
        }
        public static XViewBuilder View()
        {
            LoadMenus();
            return Column(static () =>
            {
                Text("专注.net平台的函数组合UI")
                .H3()
                .Padding(10)
                .FontColor(xTheme.Colors.White)
                .TextAlignment(XAlignment.Center)
                .Size(280, WRAP)
                .Background(new XBrush()
                {
                    StartColor = xTheme.Colors.Primary,
                    EndColor = xTheme.Colors.PrimaryDark,
                    Direction = XGradientDirection.Vertical
                })
                .Shadow(xTheme.Shadows.Button)
                .Radius(xTheme.Radius.Middle)
                .Margin(10);

                LazyColumn(menusState, static menus =>
                {
                    LazyItem(menus, item =>
                    {
                        Text(item.Name).Also(n =>
                        {
                            if (item.IsGroup)
                            {
                                n.H3().Margin(0).EnableEvent(false);
                            }
                            else
                            {
                                n.TextBody()
                                .Margin(horizontal: 20)
                                .EnableEvent(true)
                                .Radius(item.IsSelected ? xTheme.Radius.Middle : 0)
                                .Background(item.IsSelected ? xTheme.Colors.PrimaryLight3 : XColor.Empty)
                                .FontColor(item.IsSelected ? xTheme.Colors.Primary : xTheme.Colors.PrimaryText)
                                .Hand()
                                .FontWeight(item.IsSelected?xTheme.Weights.Large: xTheme.Weights.Middle)
                                .Click((builder,info) =>
                                {
                                    menusState.Value.ForEach(n => n.IsSelected = false);
                                    item.IsSelected = true;
                                    builder.NotifyLazy();
                                    DescriptionManager.ToIndexForContent(item.Name);
                                }, false);
                            }
                        }).Padding(16,10).Width(FILL);
                    });
                })
                .Weight(1).Margin(bottom:10).Space(10);
            })
            .Width(300)
            .MinWidth(300)
            .MaxWidth(600)
            .RightBorder()
            .Resize(right:true);
        }
    }
}
