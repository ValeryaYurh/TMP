namespace LR2.Entities
{
    public class Tarif
    {
        public string Direction { get; set; }
        public double CostPerKg { get; set; }

        public Tarif(string direction, double costPerKg)
        {
            Direction = direction;
            CostPerKg = costPerKg;
        }
    }
}
