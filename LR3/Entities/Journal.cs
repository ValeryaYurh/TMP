using System;
using System.Collections.Generic;

namespace LR3.Entities
{
    public class Journal
    {
        private readonly List<string> _records = new List<string>();

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
