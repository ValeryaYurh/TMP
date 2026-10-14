using System.Collections.Generic;
using System.Linq;
using LR3.Contracts;
using LR3.Events;

namespace LR3.Entities
{
    public class TransportCompany : ITransportService
    {
        private readonly Dictionary<string, Tarif> _tarifs = new Dictionary<string, Tarif>();
        private readonly List<Client> _clients = new List<Client>();

        public event EventHandler<CompanyDataChangedEventArgs>? DataChanged;

        public event EventHandler<OrderPlacedEventArgs>? OrderPlaced;

        public void AddTarif(string direction, double costPerKg)
        {
            _tarifs[direction] = new Tarif(costPerKg);
            DataChanged?.Invoke(this, new CompanyDataChangedEventArgs(
                $"добавлен тариф \"{direction}\" ({costPerKg} руб/кг)"));
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
            DataChanged?.Invoke(this, new CompanyDataChangedEventArgs(
                $"добавлен клиент \"{client.Name}\""));
        }

        public void MakeOrder(Client client, string direction, double weight)
        {
            Order order = new Order(direction, weight);
            client.Orders.Add(order);

            double cost = weight * _tarifs[direction].CostPerKg;
            OrderPlaced?.Invoke(this, new OrderPlacedEventArgs(client.Name, direction, weight, cost));
        }

        public List<string> GetTarifNamesSortedByCost()
        {
            var query =
                from tarif in _tarifs
                orderby tarif.Value.CostPerKg
                select tarif.Key;

            return query.ToList();
        }

        public double GetTotalRevenue()
        {
            var query =
                from client in _clients
                from order in client.Orders
                select order.Weight * _tarifs[order.Direction].CostPerKg;

            return query.Sum();
        }

        public double GetClientTotalCost(Client client)
        {
            var query =
                from order in client.Orders
                select order.Weight * _tarifs[order.Direction].CostPerKg;

            double rawSum = query.Sum();

            if (client.Type == ClientType.VIP)
                return rawSum * 0.9;

            return rawSum;
        }

        public string? GetClientWithMaxPayment()
        {
            var query =
                from client in _clients
                orderby GetClientTotalCost(client) descending
                select client;

            Client? topClient = query.FirstOrDefault();

            if (topClient == null)
                return null;

            return topClient.Name;
        }

        public int GetClientsCountAbovePayment(double threshold)
        {
            return _clients.Aggregate(0, delegate (int accumulated, Client client)
            {
                if (GetClientTotalCost(client) > threshold)
                    return accumulated + 1;

                return accumulated;
            });
        }

        public Dictionary<string, double> GetClientAmountsByTarif(Client client)
        {
            var query =
                from order in client.Orders
                group order by order.Direction into g
                select g;

            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (var group in query)
            {
                double sum = 0;

                foreach (Order order in group)
                    sum += order.Weight * _tarifs[order.Direction].CostPerKg;

                result[group.Key] = sum;
            }

            return result;
        }

        public List<Client> GetClients()
        {
            return _clients;
        }
    }
}
