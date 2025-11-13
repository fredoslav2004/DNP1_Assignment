using System;

namespace Utils;

public static class Utils
{
    public static string GetPersistantFilePath(string relativePath = "")
    {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string fullPath = Path.Combine(folder, relativePath);
        return fullPath;
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }
}
