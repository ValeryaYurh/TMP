namespace LR4.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock { get; set; }

        public Product(string name, int price, int quantity, bool inStock)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            InStock = inStock;
        }
    }
}
