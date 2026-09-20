using System.Collections.Generic;

namespace LR2.Interfaces
{
    // Обобщённый интерфейс пользовательской коллекции (задание 1.1, п. c).
    // Наследует IEnumerable<T>, чтобы foreach работал через сам интерфейс,
    // а не только через конкретный класс MyCustomCollection<T> (задание 2.1, п. a).
    public interface ICustomCollection<T> : IEnumerable<T>
    {
        T this[int index] { get; set; }

        int Count { get; }

        void Reset();
        void Next();
        T Current();

        void Add(T item);
        void Remove(T item);
        T RemoveCurrent();
    }
}
