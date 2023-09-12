using System;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
// работа с дисками и папками
namespace Module8
{
    class Drive
    {
        public string diskName { get; }
        public int diskSize { get; }
        public int diskFreeSpace { get; }


        public Drive(string diskName, int diskSize, int diskFreeSpace)
        {
            string DiskName = diskName;
            int DiskSize = diskSize;
            int DiskFreeSpace = diskFreeSpace;
        }

        Dictionary<string, Folder> Folders = new Dictionary<string, Folder>();
        public void CreateFolder(string name)
        {
            Folders.Add(name, new Folder());

        }
    }

    public class Folder
    {
        public List<string> Files { get; set; } = new List<string>();


    }
    class Program
    {
        static void Main(string[] args)
        {
            GetCatalogs(); //   Вызов метода получения директорий

            CreateFolder();
            DeleteFolder();

            ToTrash(@"C:\Users\Administrator\Desktop\testFolder");
        }

        static public void GetCatalogs()
        {
            string dirName = "C:\\";
            try
            {

                if (Directory.Exists(dirName))
                {
                    string[] folders = Directory.GetDirectories(dirName);
                    string[] files = Directory.GetFiles(dirName);

                    int numberFolders = folders.Length;
                    int numberFiles = files.Length;

                    Console.WriteLine($"In directory {dirName} {numberFolders} folders and {numberFiles} files");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void CreateFolder()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(@"C:\MyNewDirectory");
                if (!dirInfo.Exists)
                    dirInfo.Create();
                Console.WriteLine();
                Console.WriteLine($"New folder: { dirInfo}");

                Console.WriteLine("New directory information: ");
                GetCatalogs();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void DeleteFolder()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(@"C:\MyNewDirectory");
                dirInfo.Delete(true); // Удаление со всем содержимым
                Console.WriteLine();
                Console.WriteLine("Directory deleted");

                GetCatalogs();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void ToTrash(string name)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(name);
                string recycleBinFolder = ""; // не нашел путь корзины в Windows 11
                dirInfo.MoveTo(recycleBinFolder);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

    }

    }