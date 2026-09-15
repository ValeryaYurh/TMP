using System.Numerics;
using _553503_YURHILEVICH_Lab1.Interfaces;

namespace _553503_YURHILEVICH_Lab1.Utils
{
    // Вычисление сумм с использованием интерфейсов пространства имён
    // System.Numerics (Generic Math), как того требует задание.
    public static class GenericMathHelper
    {
        public static TNumber Sum<T, TNumber>(ICustomCollection<T> collection, Func<T, TNumber> selector)
            where TNumber : INumber<TNumber>
        {
            TNumber sum = TNumber.Zero;

            for (int i = 0; i < collection.Count; i++)
                sum += selector(collection[i]);

            return sum;
        }
    }
}
