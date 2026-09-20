using System;
using LR2.Collections;
using LR2.Interfaces;

namespace LR2.Entities
{
    public class Journal
    {
        private readonly ICustomCollection<string> _records = new MyCustomCollection<string>();

        public void LogEvent(string entityName, string description)
        {
            string record = $"[{DateTime.Now:HH:mm:ss}] {entityName}: {description}";
            _records.Add(record);
        }

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
