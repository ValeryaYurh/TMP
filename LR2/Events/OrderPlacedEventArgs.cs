using System;

namespace LR2.Events
{
    // Аргумент события "клиент заказал перевозку" (задание 2.1, п. c)
    public class OrderPlacedEventArgs : EventArgs
    {
        public string ClientName { get; }
        public string Direction { get; }
        public double Weight { get; }
        public double Cost { get; }

        public OrderPlacedEventArgs(string clientName, string direction, double weight, double cost)
        {
            ClientName = clientName;
            Direction = direction;
            Weight = weight;
            Cost = cost;
        }
    }
}
