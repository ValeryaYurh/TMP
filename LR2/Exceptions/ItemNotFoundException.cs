using System;

namespace LR2.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
            : base("Элемент отсутствует в коллекции")
        {
        }

        public ItemNotFoundException(string message)
            : base(message)
        {
        }

        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
