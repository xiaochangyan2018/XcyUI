using XchyUI.models;
using XchyUI.Components.utils;
using XchyUI.views;
using XchyUI.widgets;
using XchyUI.widgets.extensions;
using static System.Net.Mime.MediaTypeNames;
using static XchyUI.models.XFunctions;
using static XchyUI.Components.Compoments;
using static XchyUI.widgets.XWidget;

namespace XchyUI.Components
{
    public static class XViewBuilderExtensions
    {
        /// <summary>
        /// 设置输入类型
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="type"></param>
        /// <param name="onValidate"></param>
        /// <returns></returns>
        public static XViewBuilder InputType(this XViewBuilder builder,InputType type, XFunction<bool>? onValidate = null)
        {
            if (builder.View is not XInput)
            {
                return builder;
            }
            var input = builder.View as XInput;
            if(type == Components.InputType.Password || type == Components.InputType.StrongPassword)
            {
                builder.PasswordKey('●');
            }
            var textState = StateValueOf(input.Text);
            builder.TextChanged((builder, text) =>
            {
                var result = InputRegex.Validate(type, text);
                if (!result && !string.IsNullOrEmpty(text))
                {
                    builder.ErrorInput();
                }
                else
                {
                    builder.PrimaryInput();
                }
                textState.Value = text;
                onValidate?.Invoke(result);
            }, "inputType_textChanged")
            .OnFocused(builder =>
            {
                var text = builder.AsView<XText>().Text;
                var result = InputRegex.Validate(type, text);
                if (!result && !string.IsNullOrEmpty(text))
                {
                    builder.ErrorInput();
                }
                else
                {
                    builder.PrimaryInput();
                }
            })
            .OnLossFocused(builder =>
            {
                var text = builder.AsView<XText>().Text;
                var result = InputRegex.Validate(type, text);
                if (!result && !string.IsNullOrEmpty(text))
                {
                    builder.ErrorInput();
                }
                else
                {
                    builder.PrimaryInput();
                }
            });
            return builder;
        }

        public static XViewBuilder Popover(this XViewBuilder builder,XState<bool> visiblePopover, XFunction content, bool enablePopover = true, bool defaultEffect = true)
        {
            var view = builder.View;
            var rectState = StateValueOf(new XRect());
            rectState.Dispose();
            PopupCard(visiblePopover, builder =>
            {
                // 计算
                var rect = view.RenderRect;
                var visibleState = StateValueOf(true);
                var animateValue = AnimateFloatOf(visibleState, animate =>
                {
                    animate.Duration = 180;
                });
                PopoverCard(() =>
                {
                    content.Invoke();
                }, rectState, enablePopover)
                .Binding(animateValue, (builder, value) =>
                {
                    var direction = PopoverUtils.GetArrowDirection(builder, rectState.Value);
                    var point = direction switch
                    {
                        PopoverUtils.ArrowDirection.Top => rect.BottomCenter,
                        PopoverUtils.ArrowDirection.Bottom => rect.TopCenter,
                        PopoverUtils.ArrowDirection.Left => rect.RightCenter,
                        PopoverUtils.ArrowDirection.Right => rect.LeftCenter,
                        _ => rect.BottomCenter
                    };
                    builder.Scale(value, point).Alpha(value);
                }); ;
            },
            disableOutClick:false,
            outSideClick: (builder,info) =>
            {
                if (!view.RenderRect.Contain(info.Point))
                {
                    visiblePopover.Value = false;
                }
            });
            return builder
                .LayoutEnd(builder =>
                {
                    rectState.Value = builder.View.RenderRect;
                })
                .Click(() =>
                {
                    visiblePopover.Value = !visiblePopover.Value;
                }, defaultEffect, "Popover_click");
        }

        public static XViewBuilder Hand(this XViewBuilder builder)
        {
            return builder
                .Color(xTheme.Colors.PrimaryText)
                .HoverCursor(XCursorType.Hand)
                .HoverColor(xTheme.Colors.Primary);
        }
    }
}
