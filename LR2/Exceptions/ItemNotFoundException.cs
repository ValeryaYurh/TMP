using System;

namespace LR2.Exceptions
{
    // Собственное исключение: бросается из MyCustomCollection<T>.Remove(T item),
    // если удаляемый элемент отсутствует в коллекции (задание 2.1, п. b)
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
