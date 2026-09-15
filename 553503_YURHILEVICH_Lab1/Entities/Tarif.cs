namespace _553503_YURHILEVICH_Lab1.Entities
{
    public class Tarif
    {
        private string _direction = string.Empty;
        private double _costPerKg;

        public string Direction
        {
            get => _direction;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Направление должно быть указано");
                _direction = value;
            }
        }

        public double CostPerKg
        {
            get => _costPerKg;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Стоимость за кг должна быть положительным числом");
                _costPerKg = value;
            }
        }

        public Tarif(string direction, double costPerKg)
        {
            Direction = direction;
            CostPerKg = costPerKg;
        }
    }
}
