using System.Collections.Generic;
using LR3.Entities;

namespace LR3.Contracts
{
    public interface ITransportService
    {
        void AddTarif(string direction, double costPerKg);
        void AddClient(Client client);
        void MakeOrder(Client client, string direction, double weight);

        List<string> GetTarifNamesSortedByCost();
        double GetTotalRevenue();
        double GetClientTotalCost(Client client);
        string? GetClientWithMaxPayment();
        int GetClientsCountAbovePayment(double threshold);
        Dictionary<string, double> GetClientAmountsByTarif(Client client);
    }
}
