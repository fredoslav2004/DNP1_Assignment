using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CLI.UI.Core;

public class CLIUtils
{
    public static void PrintRepeatChar(char c, int count, bool newLineAfter = true)
    {
        for (int i = 0; i < count; i++)
        {
            Console.Write(c);
        }

        if (newLineAfter)
        {
            Console.WriteLine();
        }
    }
    public static void FormatInTheMiddle(string text, int totalWidth, char charToFill = ' ', bool newLineAfter = true)
    {
        if (text.Length >= totalWidth)
        {
            Console.WriteLine(text);
            return;
        }

        int spaces = (totalWidth - text.Length) / 2;
        int extraSpace = (totalWidth - text.Length) % 2;
        PrintRepeatChar(charToFill, spaces, false);
        Console.Write(text);
        PrintRepeatChar(charToFill, spaces + extraSpace, newLineAfter);
    }
    public static void DrawBox(string text, int width = 20)
    {
        Console.Write("┌"); CLIUtils.PrintRepeatChar('─', width, false); Console.Write("┐\n");
        Console.Write("│"); CLIUtils.FormatInTheMiddle(text, width, ' ', false); Console.Write("│\n");
        Console.Write("└"); CLIUtils.PrintRepeatChar('─', width, false); Console.Write("┘\n");
    }
    public static void WriteSeparator(int[] widths, char left, char mid, char right, char fill, int padding = 2)
    {
        Console.Write(left);
        for (int c = 0; c < widths.Length; c++)
        {
            PrintRepeatChar(fill, widths[c] + padding, false);
            Console.Write(c == widths.Length - 1 ? right : mid);
        }
        Console.WriteLine();
    }
    public static void DrawTable(string[,] data)
    {
        // Basic checks
        if (data is null) return;
        var rows = data.GetLength(0);
        var cols = data.GetLength(1);
        if (rows == 0 || cols == 0) return;

        // Sets the column widths to the max length of each column's data
        var widths = new int[cols];
        for (var r = 0; r < rows; r++)
            for (var c = 0; c < cols; c++)
                widths[c] = Math.Max(widths[c], (data[r, c]?.Length) ?? 0);

        // Draw the table
        WriteSeparator(widths, '┌', '┬', '┐', '─');

        for (int r = 0; r < rows; r++)
        {
            Console.Write('│');
            for (int c = 0; c < cols; c++)
            {
                var cell = data[r, c] ?? string.Empty;
                Console.Write(' ');
                Console.Write(cell.PadRight(widths[c])); // I assume right padding should be enough for now
                Console.Write(' ');
                Console.Write('│');
            }
            Console.WriteLine();

            if (r != rows - 1)
                WriteSeparator(widths, '├', '┼', '┤', '─');
        }

        WriteSeparator(widths, '└', '┴', '┘', '─');
    }
    public static void PrintError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n ⚠  {message}");
        Console.ForegroundColor = originalColor;
    }
    public static void PrintInfo(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n 🌐 {message}");
        Console.ForegroundColor = originalColor;
    }
#pragma warning disable CA1416 // Validate platform compatibility
    public static void MaximizeWindow() => ConsoleWindow.Maximize();
    public static void MinimizeWindow() => ConsoleWindow.Minimize();
#pragma warning restore CA1416 // Validate platform compatibility
}
