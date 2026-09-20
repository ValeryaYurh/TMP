using LR2.Collections;
using LR2.Contracts;
using LR2.Events;
using LR2.Interfaces;
using LR2.Utils;

namespace LR2.Entities
{
    public class TransportCompany : ITransportService
    {
        private readonly ICustomCollection<Tarif> _tarifs = new MyCustomCollection<Tarif>();
        private readonly ICustomCollection<Client> _clients = new MyCustomCollection<Client>();

        public event EventHandler<CompanyDataChangedEventArgs>? DataChanged;

        public event EventHandler<OrderPlacedEventArgs>? OrderPlaced;

        public void AddTarif(Tarif tarif)
        {
            _tarifs.Add(tarif);
            DataChanged?.Invoke(this, new CompanyDataChangedEventArgs(
                $"добавлен тариф \"{tarif.Direction}\" ({tarif.CostPerKg} руб/кг)"));
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
            DataChanged?.Invoke(this, new CompanyDataChangedEventArgs(
                $"добавлен клиент \"{client.Name}\""));
        }

        public void MakeOrder(Client client, Tarif tarif, double weight)
        {
            Order order = new Order(tarif, weight);
            client.AddOrder(order);
            OrderPlaced?.Invoke(this, new OrderPlacedEventArgs(
                client.Name, tarif.Direction, weight, order.GetCost()));
        }

        public double GetClientOrderSum(Client client)
        {
            return client.GetOrdersSum();
        }

        public double GetTotalOrdersSum()
        {
            return GenericMathHelper.Sum(_clients, delegate (Client c)
            {
                return c.GetOrdersSum();
            });
        }

        public double GetSumByDirection(string direction)
        {
            return GenericMathHelper.Sum(_clients, delegate (Client client)
            {
                return GenericMathHelper.Sum(client.GetOrders(), delegate (Order order)
                {
                    if (order.Tarif.Direction.Equals(direction, StringComparison.OrdinalIgnoreCase))
                        return order.GetCost();

                    return 0.0;
                });
            });
        }

        public Client? FindClientByName(string name)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return _clients[i];
            }

            return null;
        }

        public ICustomCollection<Client> GetClients()
        {
            return _clients;
        }

        public ICustomCollection<Tarif> GetTarifs()
        {
            return _tarifs;
        }
    }
}
