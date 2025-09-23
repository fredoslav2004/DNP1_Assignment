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
}
