using LR2.Entities;

namespace LR2.Contracts
{
    public interface ITransportService
    {
        void AddTarif(Tarif tarif);
        void AddClient(Client client);
        void MakeOrder(Client client, Tarif tarif, double weight);

        double GetClientOrderSum(Client client);
        double GetTotalOrdersSum();
        double GetSumByDirection(string direction);
    }
}
