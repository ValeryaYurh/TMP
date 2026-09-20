namespace _553503_YURHILEVICH_Lab1.Interfaces
{
    public interface ICustomCollection<out T>
    {
        T this[int index] { get; }

        int Count { get; }

        void Reset();
        void Next();
        T Current();

        void Add(T item);
        void Remove(T item);
        T RemoveCurrent();
    }
}
