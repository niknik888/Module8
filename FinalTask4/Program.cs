using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FinalTask
{
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Путь к бинарному файлу базы данных студентов
            string binaryFilePath = "students.dat";

            // Создаем директорию Students на рабочем столе
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string studentsDirectoryPath = Path.Combine(desktopPath, "Students");
            Directory.CreateDirectory(studentsDirectoryPath);

            // Читаем студентов из бинарного файла
            List<Student> students = ReadStudentsFromBinaryFile(binaryFilePath);

            // Группируем студентов по группам
            Dictionary<string, List<Student>> groupedStudents = GroupStudentsByGroup(students);

            // Создаем текстовые файлы для каждой группы
            foreach (var group in groupedStudents)
            {
                string groupFilePath = Path.Combine(studentsDirectoryPath, $"{group.Key}.txt");
                WriteStudentsToTextFile(groupFilePath, group.Value);
            }

            Console.WriteLine("Обработка файла завершена.");
        }

        static List<Student> ReadStudentsFromBinaryFile(string filePath)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string json = sr.ReadToEnd();
                    students = JsonConvert.DeserializeObject<List<Student>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }

            return students;
        }

        static Dictionary<string, List<Student>> GroupStudentsByGroup(List<Student> students)
        {
            Dictionary<string, List<Student>> groupedStudents = new Dictionary<string, List<Student>>();

            foreach (var student in students)
            {
                if (!groupedStudents.ContainsKey(student.Group))
                {
                    groupedStudents[student.Group] = new List<Student>();
                }
                groupedStudents[student.Group].Add(student);
            }

            return groupedStudents;
        }

        static void WriteStudentsToTextFile(string filePath, List<Student> students)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    foreach (var student in students)
                    {
                        string studentInfo = $"{student.Name}, {student.DateOfBirth:yyyy-MM-dd}";
                        sw.WriteLine(studentInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в текстовый файл: {ex.Message}");
            }
        }
    }
}
