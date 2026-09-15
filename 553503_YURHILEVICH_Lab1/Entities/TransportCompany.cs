using _553503_YURHILEVICH_Lab1.Collections;
using _553503_YURHILEVICH_Lab1.Contracts;
using _553503_YURHILEVICH_Lab1.Interfaces;
using _553503_YURHILEVICH_Lab1.Utils;

namespace _553503_YURHILEVICH_Lab1.Entities
{
    // Реализует интерфейс из Contracts/ITransportService.cs (задание 1.1, п. e, f)
    public class TransportCompany : ITransportService
    {
        private readonly ICustomCollection<Tarif> _tarifs = new MyCustomCollection<Tarif>();
        private readonly ICustomCollection<Client> _clients = new MyCustomCollection<Client>();

        public void AddTarif(Tarif tarif)
        {
            if (tarif == null) throw new ArgumentNullException(nameof(tarif));
            _tarifs.Add(tarif);
        }

        public void AddClient(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _clients.Add(client);
        }

        public void MakeOrder(Client client, Tarif tarif, double weight)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            Order order = new Order(tarif, weight);
            client.AddOrder(order);
        }

        public double GetClientOrderSum(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            return client.GetOrdersSum();
        }

        // Суммарная стоимость всех заказов фирмы (Generic Math)
        public double GetTotalOrdersSum()
        {
            return GenericMathHelper.Sum(_clients, c => c.GetOrdersSum());
        }

        // Стоимость заказов на определённое направление
        public double GetSumByDirection(string direction)
        {
            return GenericMathHelper.Sum(_clients, client =>
                GenericMathHelper.Sum(client.GetOrders(), order =>
                    order.Tarif.Direction.Equals(direction, StringComparison.OrdinalIgnoreCase)
                        ? order.GetCost()
                        : 0.0));
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

        public ICustomCollection<Client> GetClients() => _clients;
        public ICustomCollection<Tarif> GetTarifs() => _tarifs;
    }
}
