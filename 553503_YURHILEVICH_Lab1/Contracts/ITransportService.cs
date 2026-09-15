using _553503_YURHILEVICH_Lab1.Entities;

namespace _553503_YURHILEVICH_Lab1.Contracts
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
