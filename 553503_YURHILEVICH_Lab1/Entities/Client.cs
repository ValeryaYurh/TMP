using _553503_YURHILEVICH_Lab1.Collections;
using _553503_YURHILEVICH_Lab1.Interfaces;
using _553503_YURHILEVICH_Lab1.Utils;

namespace _553503_YURHILEVICH_Lab1.Entities
{
    public enum ClientType
    {
        Regular,
        VIP
    }

    public class Client
    {
        private string _name = string.Empty;
        private readonly ICustomCollection<Order> _orders = new MyCustomCollection<Order>();

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя клиента должно быть указано");
                _name = value;
            }
        }

        public ClientType Type { get; set; }

        public Client(string name, ClientType type)
        {
            Name = name;
            Type = type;
        }

        public void AddOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            _orders.Add(order);
        }

        public ICustomCollection<Order> GetOrders() => _orders;

        // Сумма заказов клиента (используется Generic Math через GenericMathHelper)
        public double GetOrdersSum()
        {
            double sum = GenericMathHelper.Sum(_orders, o => o.GetCost());
            return Type == ClientType.VIP ? sum * 0.9 : sum;
        }
    }
}
