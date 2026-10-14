using System;

namespace LR3.Events
{
    public class CompanyDataChangedEventArgs : EventArgs
    {
        public string Description { get; }

        public CompanyDataChangedEventArgs(string description)
        {
            Description = description;
        }
    }
}
