// Напишите программу, которая считает размер папки на диске (вместе со всеми вложенными папками и файлами).
// На вход метод принимает URL директории, в ответ — размер в байтах.



using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\Users\Administrator\Desktop\30min";
        var totalSize = DirectorySize(path);
        Console.WriteLine($"Total size: {totalSize}");


    }

    static long DirectorySize(string path)
    {
        long result = 0;
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            var directoryFiles = directoryInfo.GetFiles();
            foreach (var file in directoryFiles)
            {
                result += file.Length;
            }

            var subDirectories = directoryInfo.GetDirectories();
            foreach (var subDirectory in subDirectories)
            {
                result += DirectorySize(subDirectory.FullName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return result;
    }
}