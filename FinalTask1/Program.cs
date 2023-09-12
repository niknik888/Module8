using System;
using System.IO;

class Program 
{
    static void Main(string[] args)
    {
        string path = @"C:\Users\Administrator\Desktop\30min";

    }

    static void CleanFolder(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);


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
        foreach (string file in Directory.GetFiles(path))
        {
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.LastAccessTime < deleteTime)
            {
                fileInfo.Delete();
                Console.WriteLine($"Удален файл: {file}");
            }
        }

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

}