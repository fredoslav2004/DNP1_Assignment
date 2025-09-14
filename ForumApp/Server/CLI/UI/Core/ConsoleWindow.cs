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
    private const int SW_FORCEMINIMIZE = 11;

    [DllImport("kernel32.dll")] private static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")] private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    public static void Maximize()
    {
        var h = GetConsoleWindow();
        if (h == IntPtr.Zero) return;

        ShowWindow(h, SW_MAXIMIZE);

        // Optional: make buffer == visible area so no scrollbars after maximize
        int w = Console.LargestWindowWidth;
        int hgt = Console.LargestWindowHeight;
        Console.SetBufferSize(w, hgt);
        Console.SetWindowSize(w, hgt);
    }

    public static void Minimize()
    {
        var h = GetConsoleWindow();
        if (h == IntPtr.Zero) return; // e.g., Windows Terminal/pty: no native window to minimize
        if (!ShowWindowAsync(h, SW_MINIMIZE))
            ShowWindow(h, SW_MINIMIZE); // fallback
    }

    public static void ForceMinimize()
    {
        var h = GetConsoleWindow();
        if (h == IntPtr.Zero) return;
        if (!ShowWindowAsync(h, SW_FORCEMINIMIZE))
            ShowWindow(h, SW_FORCEMINIMIZE);
    }

    public static void Restore()
    {
        var h = GetConsoleWindow();
        if (h == IntPtr.Zero) return;
        ShowWindow(h, SW_RESTORE);
    }
}