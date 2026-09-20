using LR2.Collections;
using LR2.Interfaces;
using LR2.Utils;

namespace LR2.Entities
{
    public enum ClientType
    {
        Regular,
        VIP
    }

    public class Client
    {
        private readonly ICustomCollection<Order> _orders = new MyCustomCollection<Order>();

        public string Name { get; set; }
        public ClientType Type { get; set; }

        public Client(string name, ClientType type)
        {
            Name = name;
            Type = type;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public ICustomCollection<Order> GetOrders()
        {
            return _orders;
        }

        public double GetOrdersSum()
        {
            double sum = GenericMathHelper.Sum(_orders, delegate (Order o)
            {
                return o.GetCost();
            });

            if (Type == ClientType.VIP)
                return sum * 0.9;

            return sum;
        }
    }
}
