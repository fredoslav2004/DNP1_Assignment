using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace CLI.UI.Core;

[SupportedOSPlatform("windows")]
public static class ConsoleWindow
{
    private const int SW_MAXIMIZE = 3;
    private const int SW_MINIMIZE = 6;
    private const int SW_RESTORE = 9;
    private const int MAX_DIM = short.MaxValue - 1;

    [DllImport("kernel32.dll")] private static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static void Maximize()
    {
        var h = GetConsoleWindow();
        if (h != IntPtr.Zero) ShowWindow(h, SW_MAXIMIZE);
        var w = Math.Clamp(Console.LargestWindowWidth, 1, MAX_DIM);
        var hgt = Math.Clamp(Console.LargestWindowHeight, 1, MAX_DIM);
        ResizeSafely(w, hgt);
    }

    public static void Minimize()
    {
        var h = GetConsoleWindow();
        if (h != IntPtr.Zero) ShowWindow(h, SW_MINIMIZE);
    }

    public static void Restore()
    {
        var h = GetConsoleWindow();
        if (h != IntPtr.Zero) ShowWindow(h, SW_RESTORE);
    }

    private static void ResizeSafely(int targetW, int targetH)
    {
        var curWinW = Console.WindowWidth;
        var curWinH = Console.WindowHeight;
        var curBufW = Console.BufferWidth;
        var curBufH = Console.BufferHeight;

        targetW = Math.Clamp(targetW, 1, MAX_DIM);
        targetH = Math.Clamp(targetH, 1, MAX_DIM);

        if (curWinW > targetW || curWinH > targetH)
        {
            Console.SetWindowSize(Math.Min(curWinW, targetW), Math.Min(curWinH, targetH));
            curWinW = Console.WindowWidth;
            curWinH = Console.WindowHeight;
        }

        int needBufW = Math.Max(targetW, curWinW);
        int needBufH = Math.Max(targetH, curWinH);
        if (curBufW < needBufW || curBufH < needBufH)
        {
            Console.SetBufferSize(Math.Clamp(needBufW, 1, MAX_DIM), Math.Clamp(needBufH, 1, MAX_DIM));
            curBufW = Console.BufferWidth;
            curBufH = Console.BufferHeight;
        }

        if (Console.WindowWidth != targetW || Console.WindowHeight != targetH)
            Console.SetWindowSize(targetW, targetH);

        if (curBufW != targetW || curBufH != targetH)
            Console.SetBufferSize(targetW, targetH);
    }
}
