namespace _553503_YURHILEVICH_Lab1.Interfaces
{
    // Обобщённый интерфейс пользовательской коллекции (задание 1.1, п. c)
    public interface ICustomCollection<T>
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
