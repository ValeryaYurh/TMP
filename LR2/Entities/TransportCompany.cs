using LR2.Collections;
using LR2.Contracts;
using LR2.Events;
using LR2.Interfaces;
using LR2.Utils;

namespace LR2.Entities
{
    // Реализует интерфейс из Contracts/ITransportService.cs (задание 1.1, п. e, f)
    public class TransportCompany : ITransportService
    {
        private readonly ICustomCollection<Tarif> _tarifs = new MyCustomCollection<Tarif>();
        private readonly ICustomCollection<Client> _clients = new MyCustomCollection<Client>();

        // События варианта 7 (задание 2.1, п. c, индивидуальное задание №7).
        // Используются стандартные делегаты EventHandler<TEventArgs>.

        // При изменении списка тарифов или списка клиентов — подписан Journal
        public event EventHandler<CompanyDataChangedEventArgs>? DataChanged;

        // При заказе перевозки клиентом — подписан Program
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
