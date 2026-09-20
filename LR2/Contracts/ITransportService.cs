using LR2.Entities;

namespace LR2.Contracts
{
    // Интерфейс, описывающий функции системы согласно варианту 7
    // "Фирма грузоперевозок" (задание 1.1, п. e)
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
