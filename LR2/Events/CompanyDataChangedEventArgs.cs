using System;

namespace LR2.Events
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
