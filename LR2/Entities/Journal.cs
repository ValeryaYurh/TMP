using System;
using LR2.Collections;
using LR2.Interfaces;

namespace LR2.Entities
{
    // Журнал событий фирмы (задание 2.1, п. d)
    public class Journal
    {
        private readonly ICustomCollection<string> _records = new MyCustomCollection<string>();

        // Запись события в журнал
        public void LogEvent(string entityName, string description)
        {
            string record = $"[{DateTime.Now:HH:mm:ss}] {entityName}: {description}";
            _records.Add(record);
        }

        // Вывод в консоль списка всех зарегистрированных событий.
        // Единственный метод в проекте, которому разрешено печатать самому —
        // это прямое требование задания к классу Journal.
        public void PrintAllEvents()
        {
            if (_records.Count == 0)
            {
                Console.WriteLine("  Журнал пуст");
                return;
            }

            foreach (string record in _records)
                Console.WriteLine($"  {record}");
        }
    }
}
