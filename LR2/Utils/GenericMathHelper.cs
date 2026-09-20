using System.Numerics;
using LR2.Interfaces;

namespace LR2.Utils
{
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
