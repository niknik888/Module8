using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\Users\Administrator\Desktop\30min";

        var totalSizeBefore = DirectorySize(path);
        Console.WriteLine($"Размер папки перед очисткой: {totalSizeBefore} байт");

        CleanFolder(path);

        var totalSizeAfter = DirectorySize(path);

        Console.WriteLine($"Освобождено: {totalSizeBefore - totalSizeAfter} байт");
        Console.WriteLine($"Размер папки после очистки: {totalSizeAfter} байт");



    }

    static void CleanFolder(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);

        Console.WriteLine("Очистка запущена...");
        try
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Указанная папка не существует.");
            }
            else
            {
                DateTime deleteTime = DateTime.Now - TimeSpan.FromMinutes(30);

                DeleteFolder(path, deleteTime);

                Console.WriteLine("Очистка завершена.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Недостаточно прав доступа для очистки указанной папки.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static void DeleteFolder(string path, DateTime deleteTime)
    {
        int deleteCounter = 0;
        foreach (string file in Directory.GetFiles(path))
        {
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.LastAccessTime < deleteTime)
            {
                fileInfo.Delete();
                deleteCounter++;
                Console.WriteLine($"Удален файл: {file}");
            }
        }
        Console.WriteLine($"Удалено файлов: {deleteCounter}");

        foreach (string subfolder in Directory.GetDirectories(path))
        {
            DeleteFolder(subfolder, deleteTime);
        }
        

        if (Directory.GetFileSystemEntries(path).Length == 0)
        {
            Directory.Delete(path);
            Console.WriteLine($"Удалена пустая папка: {path}");
        }
        
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