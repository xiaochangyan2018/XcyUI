using ExCSS;
using XchyUI.Components;
using XchyUI.Demo.elmedemo;
using XchyUI.models;
using XchyUI.widgets;
using XchyUI.widgets.extensions;
using static XchyUI.Components.Compoments;
using static XchyUI.widgets.XWidget;

namespace XchyUIDemo.elmedemo.description
{
    public class ChartDescription
    {
        public static List<DescriptionInfo> GetInfos()
        {
            return new List<DescriptionInfo>()
            {
                new DescriptionInfo()
                {
                    Title = "LineChart 折线图",
                    Tag = "LineChart",
                    Desription = "LineChart 简单示例",
                    ContentFunction = ()=>
                    {
                        int width = 800;
                        int height = 400;
                        int yLabelWidth = 40;
                        int[] yValues = [100, 75, 50, 25, 0];
                        var itemHeight = height / yValues.Length;
                        List<float> values = [100, 50, 75, 0, 100];
                        Chart(width, height, () =>
                        {
                            YAxis(yValues, yLabelWidth, itemHeight);
                            Lines(values, yLabelWidth, itemHeight, 5, 0, 100);
                            XAxis([2, 6, 8, 10, 12], yLabelWidth);
                        });
                    },
                    Code = @"
int width = 800;
int height = 400;
int yLabelWidth = 40;
int[] yValues = [100, 75, 50, 25, 0];
var itemHeight = height / yValues.Length;
List<float> values = [100, 50, 75, 0, 100];
Chart(width, height, () =>
{
    YAxis(yValues, yLabelWidth, itemHeight);
    Lines(values, yLabelWidth, itemHeight, 5, 0, 100);
    XAxis([2, 6, 8, 10, 12], yLabelWidth);
});
"
                },
                new DescriptionInfo()
                {
                    Title = "BarChart 柱形图",
                    Tag = "BarChart",
                    Desription = "BarChart 简单示例",
                    ContentFunction = ()=>
                    {
                        int width = 800;
                        int height = 400;
                        int yLabelWidth = 40;
                        int[] yValues = [100, 75, 50, 25, 0];
                        var itemHeight = height / yValues.Length;
                        List<float> values = [100, 50, 75, 0, 100];
                        Chart(width, height, () =>
                        {
                            YAxis(yValues, yLabelWidth, itemHeight);
                            VerticalBars(values, yLabelWidth, itemHeight, 5, 0, 100);
                            XAxis([2, 6, 8, 10, 12], yLabelWidth);
                        });
                    },
                    Code = @"
int width = 800;
int height = 400;
int yLabelWidth = 40;
int[] yValues = [100, 75, 50, 25, 0];
var itemHeight = height / yValues.Length;
List<float> values = [100, 50, 75, 0, 100];
Chart(width, height, () =>
{
    YAxis(yValues, yLabelWidth, itemHeight);
    VerticalBars(values, yLabelWidth, itemHeight, 5, 0, 100);
    XAxis([2, 6, 8, 10, 12], yLabelWidth);
});
"
                },

                new DescriptionInfo()
                {
                    Title = "Scatter  散点图",
                    Tag = "Scatter",
                    Desription = "Scatter 简单示例",
                    ContentFunction = ()=>
                    {
                        int width = 800;
                        int height = 400;
                        int yLabelWidth = 40;
                        int[] yValues = [100, 75, 50, 25, 0];
                        var itemHeight = height / yValues.Length;
                        List<float> values = [23, 44, 55, 12, 75, 12,33,35,89,23,34,34,22,55,33,55,66,55,44];
                        Chart(width, height, () =>
                        {
                            YAxis(yValues, yLabelWidth, itemHeight);
                            Circels(values, yLabelWidth, itemHeight, 5, 0, 100);
                            XAxis([2, 6, 8, 10, 12], yLabelWidth);
                        });
                    },
                    Code = @"
int width = 800;
int height = 400;
int yLabelWidth = 40;
int[] yValues = [100, 75, 50, 25, 0];
var itemHeight = height / yValues.Length;
List<float> values = [23, 44, 55, 12, 75, 12,33,35,89];
Chart(width, height, () =>
{
    YAxis(yValues, yLabelWidth, itemHeight);
    Circels(values, yLabelWidth, itemHeight, 5, 0, 100);
    XAxis([2, 6, 8, 10, 12], yLabelWidth);
});
"
                },
                new DescriptionInfo()
                {
                    Title = "PieChart 饼图",
                    Tag = "PieChart",
                    Desription = "PieChart 简单示例",
                    ContentFunction = ()=>
                    {
                        List<float> values = [23, 44, 55, 12, 75];
                        List<XColor> colors = [XColors.Red, XColors.Magenta, XColors.Orange, XColors.Green, XColors.Blue];
                        PieChart(values, colors, 300, 300);
                    },
                    Code = @"
List<float> values = [23, 44, 55, 12, 75];
List<XColor> colors = [XColors.Red, XColors.Magenta, XColors.Orange, XColors.Green, XColors.Blue];
PieChart(values, colors, 300, 300);
"
                },
            };
        }
    }
}
