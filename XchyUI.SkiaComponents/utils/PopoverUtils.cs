using XchyUI.expansions;
using XchyUI.models;
using XchyUI.utils;
using XchyUI.widgets;

namespace XchyUI.Components.utils
{
    public class PopoverUtils
    {
        internal static ArrowDirection GetArrowDirection(XViewBuilder builder, XRect hoverRect)
        {
            var view = builder.View;
            var rect = view.RenderRect;
            var height = view.RootView().Height;
            var width = view.RootView().Width;
            var arrowSize = 12.AsPx();
            var space = 10.AsPx();
            var direction = ArrowDirection.Top;
            var left = hoverRect.Left - space - view.Width;
            var top = hoverRect.Top - space - view.Height;
            var right = hoverRect.Right + space + view.Width;
            var bottom = hoverRect.Bottom + space + view.Height;
            if (bottom < height)
            {
                direction = ArrowDirection.Top;
            }
            else if (top > 0)
            {
                direction = ArrowDirection.Bottom;
            }
            else if (right < width)
            {
                direction = ArrowDirection.Left;
            }
            else if (left > 0)
            {
                direction = ArrowDirection.Right;
            }
            return direction;
        }
        internal static XPoint GetPopoverLocation(XViewBuilder builder, XRect hoverRect, bool enablePopover = true)
        {
            var view = builder.View;
            var padding = builder.View.LayoutParams.Padding;
            var space = enablePopover ? 4.AsPx() : 10.AsPx();
            var marginX = hoverRect.Left - (int)padding.Left;
            var marginY = hoverRect.Bottom + space;
            var height = view.RootView().Height;
            var width = view.RootView().Width;
            var left = hoverRect.Left - space - view.Width;
            var top = hoverRect.Top - space - view.Height;
            var right = hoverRect.Right + space + view.Width;
            var bottom = hoverRect.Bottom + space + view.Height;
            if (bottom < height)
            {
                marginY = hoverRect.Bottom + space;
                if (marginX < 0 && marginX + view.Width < width)
                {
                    marginX = hoverRect.Left - (int)padding.Left;
                }
                else if (marginX > 0 && marginX + view.Width > width)
                {
                    marginX = hoverRect.Left - view.Width + hoverRect.Width + (int)padding.Right;
                }
            }
            else if (top > 0)
            {
                marginY = hoverRect.Top - space - view.Height;
                if (marginX < 0 && marginX + view.Width < width)
                {
                    marginX = hoverRect.Left - (int)padding.Left;
                }
                else if (marginX > 0 && marginX + view.Width > width)
                {
                    marginX = hoverRect.Left - view.Width + hoverRect.Width + (int)padding.Right;
                }
            }
            else if (right < width)
            {
                marginX = hoverRect.Right + space;
                marginY = hoverRect.Center.Y - view.Height / 2;
            }
            else if (left > 0)
            {
                marginX = hoverRect.X - space - view.Width;
                marginY = hoverRect.Center.Y - view.Height / 2;
            }
            return new XPoint((int)(marginX), (int)(marginY));
        }
        public static void DrawRoundedArrowBubble(
            XRect rect,
            XStyle style,
            bool isCache,
            XRect hoverRect,
            int radius,        // 气泡圆角
            int arrowSize,     // 箭头长度
            ArrowDirection dir,
            int strokeWidth = 1)
        {
            int l = rect.Left;
            int t = rect.Top;
            int r = rect.Right;
            int b = rect.Bottom;
            int w = rect.Width;
            int h = rect.Height;
            int cx = hoverRect.Center.X; // 中心X
            int cy = hoverRect.Center.Y; // 中心Y
            // 计算方向

            RenderImp.DrawPath(rect, style, isCache, () =>
            {
                RenderImp.MoveTo(l + radius, t);
                if (dir == ArrowDirection.Top)
                {
                    RenderImp.LineTo(cx - arrowSize, t);
                    RenderImp.LineTo(cx, t - arrowSize);
                    RenderImp.LineTo(cx + arrowSize, t);
                }
                RenderImp.LineTo(r - radius, t);
                RenderImp.ArcTo(r, t + radius, radius);
                if (dir == ArrowDirection.Right)
                {
                    RenderImp.LineTo(r, cy - arrowSize);
                    RenderImp.LineTo(r + arrowSize, cy);
                    RenderImp.LineTo(r, cy + arrowSize);
                }
                RenderImp.LineTo(r, b - radius);
                RenderImp.ArcTo(r - radius, b, radius);
                if (dir == ArrowDirection.Bottom)
                {
                    RenderImp.LineTo(cx + arrowSize, b);
                    RenderImp.LineTo(cx, b + arrowSize);
                    RenderImp.LineTo(cx - arrowSize, b);
                }
                RenderImp.LineTo(l + radius, b);
                RenderImp.ArcTo(l, b - radius, radius);
                if (dir == ArrowDirection.Left)
                {
                    RenderImp.LineTo(l, cy + arrowSize);
                    RenderImp.LineTo(l - arrowSize, cy);
                    RenderImp.LineTo(l, cy - arrowSize);
                }

                RenderImp.LineTo(l, t + radius);
                RenderImp.ArcTo(l + radius, t, radius);
            });
        }

        public enum ArrowDirection
        {
            Top,
            Bottom,
            Left,
            Right
        }
    }
}
