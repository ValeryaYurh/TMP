namespace _553503_YURHILEVICH_Lab1.Entities
{
    public class Order
    {
        private Tarif _tarif = null!;
        private double _weight;

        public Tarif Tarif
        {
            get => _tarif;
            set => _tarif = value ?? throw new ArgumentException("Тариф не определён");
        }

        public double Weight
        {
            get => _weight;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Вес груза должен быть положительным числом");
                _weight = value;
            }
        }

        public Order(Tarif tarif, double weight)
        {
            Tarif = tarif;
            Weight = weight;
        }

        public double GetCost()
        {
            return Tarif.CostPerKg * Weight;
        }
    }
}
