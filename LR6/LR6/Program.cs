using System.Reflection;
using LR6.Contracts;
using LR6.Entities;

namespace LR6
{
    internal class Program
    {
        static void Main()
        {
            List<Employee> employees = CreateEmployees();

            string libraryPath = Path.Combine(AppContext.BaseDirectory, "FileServiceLib.dll");
            Assembly assembly = Assembly.LoadFrom(libraryPath);

            Console.WriteLine("Лабораторная 6. Сборки и метаданные\n");
            Console.WriteLine($"Динамически загружена сборка: {assembly.FullName}");
            Console.WriteLine($"Путь: {assembly.Location}");
            Console.WriteLine("Типы в сборке:");
            foreach (Type type in assembly.GetTypes())
                Console.WriteLine($"  {type.FullName}");

            IFileService<Employee> fileService = CreateFileService(assembly);
            Console.WriteLine($"\nСоздан объект типа: {fileService.GetType()}");

            string filePath = Path.Combine(AppContext.BaseDirectory, "employees.json");

            fileService.SaveData(employees, filePath);
            Console.WriteLine($"\nКоллекция записана в файл: {filePath}");

            List<Employee> loadedEmployees = fileService.ReadFile(filePath).ToList();

            PrintEmployees("Данные, прочитанные из файла", loadedEmployees);
        }

        static List<Employee> CreateEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee("Иванов", 45, 20, true));
            employees.Add(new Employee("Петров", 32, 8, false));
            employees.Add(new Employee("Сидорова", 28, 5, false));
            employees.Add(new Employee("Кузнецова", 39, 14, true));
            employees.Add(new Employee("Орлов", 24, 2, false));
            employees.Add(new Employee("Лебедева", 51, 27, true));

            return employees;
        }

        static IFileService<Employee> CreateFileService(Assembly assembly)
        {
            Type openType = assembly.GetType("FileServiceLib.FileService`1")!;
            Type closedType = openType.MakeGenericType(typeof(Employee));

            return (IFileService<Employee>)Activator.CreateInstance(closedType)!;
        }

        static void PrintEmployees(string title, IEnumerable<Employee> employees)
        {
            Console.WriteLine($"\n{title}:");

            foreach (Employee employee in employees)
            {
                string managerText = "нет";
                if (employee.IsManager)
                    managerText = "да";

                Console.WriteLine($"  {employee.Name}: возраст {employee.Age}, стаж {employee.ExperienceYears}, руководитель: {managerText}");
            }
        }
    }
}
