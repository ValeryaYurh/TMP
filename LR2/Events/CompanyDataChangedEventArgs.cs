using System;

namespace LR2.Events
{
    // Аргумент события "изменился список тарифов или клиентов" (задание 2.1, п. c)
    public class CompanyDataChangedEventArgs : EventArgs
    {
        public string Description { get; }

        public CompanyDataChangedEventArgs(string description)
        {
            Description = description;
        }
    }
}
