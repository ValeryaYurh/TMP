namespace LR3.Entities
{
    public class Order
    {
        public string Direction { get; set; }
        public double Weight { get; set; }

        public Order(string direction, double weight)
        {
            Direction = direction;
            Weight = weight;
        }
    }
}
