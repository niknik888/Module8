using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;


/*
class FileWriter
{
   public static void Main()
   {
        var fileInfo = new FileInfo(@"C:\Users\Administrator\source\repos\Module8\Module83\Program.cs"); // Укажем путь 
      
            //FileInfo fi = new FileInfo(filePath);
            // Откроем файл и прочитаем его содержимое
            using (StreamReader sr = fileInfo.OpenText())
            {
                string str = "";
                while ((str = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной и выводим в консоль
                {
                    Console.WriteLine(str);
                }
                
            }

            
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine($"// Time of last start: {DateTime.Now}");
                
            }




    }
}
// Time of last start: 12.09.2023 10:44:17
// Time of last start: 12.09.2023 10:44:25
// Time of last start: 12.09.2023 10:48:07

*/


/*
class BinaryExperiment
{
    const string filePath = @"C:\Users\Administrator\Downloads\BinaryFile.bin";

    static void Main()
    {
        // Считываем
        ReadValues();
    }

 
    static void ReadValues()
    {

        string StringValue;

        if (File.Exists(filePath))
        {
            // Создаем объект BinaryReader и инициализируем его возвратом метода File.Open.
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                // Применяем специализированные методы Read для считывания соответствующего типа данных.

                StringValue = reader.ReadString();

            }

            FileInfo fi = new FileInfo(filePath);

            using (BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Open)))
            {
                bw.Write($"Time of update file: {DateTime.Now} in computer on {Environment.OSVersion}");

            }


            Console.WriteLine("Из файла считано:");
            Console.WriteLine(StringValue);

        }
    }
}
*/

[Serializable]
class Contact
{
    public string Name { get; set; }
    public long PhoneNumber { get; set; }
    public string Email { get; set; }

    public Contact(string name, long phoneNumber, string email)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}


class Program
{
    static void Main(string[] args)
    {
        // объект для сериализации
        var contact = new Contact("Nik", 8987541365, "nik@mail.com");
        Console.WriteLine("Объект создан");

        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        using (var fs = new FileStream("myContacts.bin", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, contact);
            Console.WriteLine("Объект сериализован");
        }

    }
}
