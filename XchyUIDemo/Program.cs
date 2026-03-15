using XchyUI.Components;
using XchyUI.GLFW.window;
using XchyUI.SkiaSharp;
using XchyUI.utils;
using XchyUIDemo;

HotkeyManager.Start();
WindowManager.Get().Init();
var window = new MainWindow();
window.RenderBackend = new SkiaRenderBackend();
WindowManager.Get().SetMainWindow(window);
XTask.Run(() =>
{
    SvgRes.Load();
});
WindowManager.Get().Start();
