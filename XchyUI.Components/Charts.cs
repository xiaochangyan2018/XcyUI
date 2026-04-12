
using System.Runtime.CompilerServices;
using XchyUI.expansions;
using XchyUI.models;
using XchyUI.theme;
using XchyUI.utils;
using XchyUI.widgets;
using XchyUI.widgets.extensions;
using static System.Net.Mime.MediaTypeNames;
using static XchyUI.models.XFunctions;
using static XchyUI.widgets.XWidget;


namespace XchyUI.Components
{
    public struct XPointF
    {
        public float X { get; set; }
        public float Y { get; set; }
        public XPointF(float x,float y)
        {
            X = x;
            Y = y;
        }
    }
    public static partial class Compoments
    {
        public static List<float> LTTBReduceValues(List<float> values, int threshold)
        {
            int dataLength = values.Count;

            // 👇 点数少于目标数量 → 直接返回，不稀释
            if (dataLength <= threshold)
                return values;

            List<float> sampled = new List<float>();
            sampled.Add(values[0]); // 保留第一个点

            int bucketSize = (dataLength - 2) / (threshold - 2);
            int aIndex = 0;

            for (int i = 1; i < threshold - 1; i++)
            {
                int bucketStart = i * bucketSize + 1;
                int bucketEnd = (i + 1) * bucketSize + 1;
                bucketEnd = Math.Min(bucketEnd, dataLength - 1);

                // 上一个点 A
                float aY = values[aIndex];
                XPointF a = new XPointF(aIndex, aY);

                // 下一个区间的终点 B（虚拟）
                int bRealIndex = Math.Min((i + 1) * bucketSize + 1, dataLength - 1);
                XPointF b = new XPointF(bRealIndex, values[bRealIndex]);

                float maxArea = -1;
                int bestIndex = bucketStart;

                // 遍历当前分段，找【面积最大 = 最能代表波形】的点
                for (int j = bucketStart; j < bucketEnd; j++)
                {
                    XPointF p = new XPointF(j, values[j]);
                    // 三角形面积公式（LTTB核心）
                    float area = Math.Abs((a.X - p.X) * (b.Y - a.Y) - (a.X - b.X) * (p.Y - a.Y));

                    if (area > maxArea)
                    {
                        maxArea = area;
                        bestIndex = j;
                    }
                }

                sampled.Add(values[bestIndex]);
                aIndex = bestIndex;
            }

            sampled.Add(values[dataLength - 1]); // 保留最后一个点
            return sampled;
        }

        public static void Chart(
            int width,
            int height, 
            XFunction content,
            int yAxisWidth = 0,
            int xAxisHeight = 0
        )
        {
            Box(content).Size(width, height).ContentAlignment(XAlignment.LeftTop);
        }

        public static void Lines(List<float> values,int yLabelWidth,int itemHeight,int itemCount,int minValue,int maxValue,float smoothness = 0f)
        {
            Spacer(FILL).Draw(builder =>
            {
                var rect = builder.View.RenderRect;
                var style = XThemeManager.DrawStyle;
                style.Reset();
                style.Border = new XBorder()
                {
                    Color = new XBrush() { StartColor = xTheme.Colors.Primary },
                    Size = new XSpace(2)
                };
                var startX = builder.View.X;
                var startY = builder.View.Y + itemHeight.AsPx() / 2;
                var marginX = yLabelWidth.AsPx();
                var width = builder.View.Width - marginX - 10.AsPx();
                var xSpace = width / values.Count;
                xSpace = Math.Max(1, xSpace);
                values = LTTBReduceValues(values, width);
                RenderImp.DrawPath(rect, style, false, () =>
                {
                    var points = new List<XPoint>();
                    for (int i = 0; i < values.Count; i++)
                    {
                        var x = (startX + marginX + xSpace / 2 + (i * xSpace));

                        var height = (float)(itemHeight * (itemCount-1)).AsPx();
                        var y = startY + (height / (maxValue - minValue)) * (maxValue - values[i]);

                        if (i == 0)
                        {
                            RenderImp.MoveTo(x, (int)y);
                        }
                        else
                        {
                            points.Add(new XPoint(x, (int)y));
                            if (smoothness == 0)
                            {
                                RenderImp.LineTo(x, (int)y);
                            }
                        }
                    }
                    ;
                    if (smoothness > 0 && points.Count > 2)
                    {
                        SmoothLinePath(points, 0.3f);
                    }
                });
            });
        }

        public static XViewBuilder VerticalBars(
            List<float> values,
            int marginLeft,
            int itemHeight,
            int itemCount,
            int minValue,
            int maxValue
        )
        {
            var height = itemHeight * (itemCount - 1)+2f;
            return Row(() =>
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var valueHeight = height / (maxValue - minValue) * (maxValue - values[i]);
                    Box(() =>
                    {
                        Spacer()
                        .Size(30,FILL).Background(xTheme.Colors.Primary);
                    }).Size(FILL)
                    .Padding(top: (int)valueHeight)
                    .ContentAlignment(XAlignment.TopCenter);
                }
            }).Size(FILL, (int)height)
            .Margin(left: marginLeft, right: 10,top: itemHeight/2)
            .Alignment(XAlignment.LeftTop)
            .HorizontalAlignment(XHorizontalAlignment.Bisect);
        }

        public static XViewBuilder Circels(
            List<float> values,
            int marginLeft,
            int itemHeight,
            int itemCount,
            int minValue,
            int maxValue
        )
        {
            var height = itemHeight * (itemCount - 1) + 2f;
            return Row(() =>
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var valueHeight = height / (maxValue - minValue) * (maxValue - values[i]);
                    Box(() =>
                    {
                        Spacer()
                        .Size(20)
                        .Border(xTheme.Colors.PrimaryDark, 2)
                        .Background(xTheme.Colors.PrimaryLight2)
                        .Circle();
                    }).Size(FILL)
                    .ClipContent(false)
                    .Padding(top: (int)valueHeight-10)
                    .ContentAlignment(XAlignment.TopCenter);
                }
            }).Size(FILL, (int)height)
            .Margin(left: marginLeft, right: 10, top: itemHeight / 2)
            .Alignment(XAlignment.LeftTop)
            .HorizontalAlignment(XHorizontalAlignment.Bisect);
        }
        public static void YAxis(int[] yAxis,int valueWidth, int itemHeight)
        {
            Column(() =>
            {
                for (int i = 0; i < yAxis.Length; i++)
                {
                    Row(() =>
                    {
                        Text(yAxis[i].ToString()).Width(valueWidth).TextAlignment(XAlignment.Center);
                        Spacer(1).Weight(1).BottomBorder();
                    }).Size(FILL, itemHeight);
                }
            }).Size(FILL, WRAP);
        }

        public static XViewBuilder XAxis(object[] xAxis, int marginLeft)
        {
            return Row(() =>
            {
                for (int i = 0; i < xAxis.Length; i++)
                {
                    Text(xAxis[i].ToString()).TextAlignment(XAlignment.Center);
                }
            }).Size(FILL, WRAP)
            .Margin(left: marginLeft, right: 10)
            .Alignment(XAlignment.LeftBottom)
            .HorizontalAlignment(XHorizontalAlignment.Bisect);
        }

        private static void SmoothLinePath(List<XPoint> points, float smoothness)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                var current = points[i];
                var next = points[i + 1];
                XPoint controlPoint1, controlPoint2;
                if (i == 0)
                {
                    controlPoint1 = current;
                    controlPoint2 = new XPoint(
                        (current.X + next.X) / 2,
                        (current.Y + next.Y) / 2
                    );
                }
                else if (i == points.Count - 2)
                {
                    controlPoint1 = new XPoint(
                        (current.X + points[i - 1].X) / 2,
                        (current.Y + points[i - 1].Y) / 2
                    );
                    controlPoint2 = next;
                }
                else
                {
                    XPoint prev = points[i - 1];
                    XPoint nextNext = points[i + 1];
                    int dx1 = (int)((next.X - prev.X) * smoothness);
                    int dy1 = (int)((next.Y - prev.Y) * smoothness);
                    int dx2 = (int)((current.X - nextNext.X) * smoothness);
                    int dy2 = (int)((current.Y - nextNext.Y) * smoothness);
                    controlPoint1 = new XPoint(current.X + dx1, current.Y + dy1);
                    controlPoint2 = new XPoint(next.X + dx2, next.Y + dy2);
                }
                RenderImp.CubicTo(controlPoint1, controlPoint2, next);
            }           
        }

        public static XViewBuilder PieChart(
            List<float> values,
            List<XColor> colors,
            int width,
            int height
        )
        {
            return Box(() =>
            {                
                var sum = values.Sum();
                var startAngle = -90f;
                for (int i = 0; i < values.Count; i++)
                {
                    var sweepAngle = 360 * values[i] / sum;
                    var color = colors[i];
                    Arc(color, startAngle, sweepAngle);
                    startAngle += sweepAngle;
                }
            }).Size(width, height);
        }
        private static void Arc(XColor color, float startAngle, float sweepAngle)
        {
            Spacer(FILL).EnableOverDraw(true).Background(color).Draw(builder =>
            {
                RenderImp.DrawArc(builder.View.RenderRect, builder.View.Style, startAngle, sweepAngle, true);
            });
        }
    }
}
