namespace LR3.Entities
{
    public class Tarif
    {
        public double CostPerKg { get; set; }

        public Tarif(double costPerKg)
        {
            CostPerKg = costPerKg;
        }
    }
}
