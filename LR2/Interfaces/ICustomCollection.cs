using System.Collections.Generic;

namespace LR2.Interfaces
{
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
