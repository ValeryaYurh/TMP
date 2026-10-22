using LR4.Entities;

namespace LR4.Comparers
{
    public class MyCustomComparer<T> : IComparer<T> where T : Product
    {
        public int Compare(T? x, T? y)
        {
            return string.Compare(x!.Name, y!.Name);
        }
    }
}
