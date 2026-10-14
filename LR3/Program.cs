using LR3.Entities;
using LR3.Events;

namespace LR3
{
    internal class Program
    {
        static void Main()
        {
            TransportCompany company = new TransportCompany();
            Journal journal = new Journal();

            company.DataChanged += delegate (object? sender, CompanyDataChangedEventArgs e)
            {
                journal.LogEvent("TransportCompany", e.Description);
            };

            company.OrderPlaced += delegate (object? sender, OrderPlacedEventArgs e)
            {
                Console.WriteLine($"[Событие] Клиент \"{e.ClientName}\" заказал перевозку: " +
                                   $"направление \"{e.Direction}\", вес {e.Weight} кг, стоимость {e.Cost:F2} руб.");
            };

            EnterData(company);

            Console.WriteLine("\nВариант 7. Фирма грузоперевозок (LINQ + стандартные коллекции)\n");

            Console.WriteLine("Тарифы, отсортированные по стоимости:");
            foreach (string direction in company.GetTarifNamesSortedByCost())
                Console.WriteLine($"  {direction}");

            Console.WriteLine($"\nОбщая стоимость всех выполненных фирмой перевозок: {company.GetTotalRevenue():F2}");

            Console.WriteLine("\nСтоимость перевозок по каждому клиенту (по действующим тарифам):");
            foreach (Client client in company.GetClients())
                Console.WriteLine($"  {client.Name}: {company.GetClientTotalCost(client):F2}");

            string? topClientName = company.GetClientWithMaxPayment();
            Console.WriteLine($"\nКлиент, заплативший максимальную сумму: {topClientName}");

            double threshold = 150;
            int aboveCount = company.GetClientsCountAbovePayment(threshold);
            Console.WriteLine($"Количество клиентов, заплативших больше {threshold}: {aboveCount}");

            Console.WriteLine("\nСуммы по каждому тарифу для клиента \"Иванов\":");
            Client ivanov = company.GetClients()[0];
            foreach (var pair in company.GetClientAmountsByTarif(ivanov))
                Console.WriteLine($"  {pair.Key}: {pair.Value:F2}");

            Console.WriteLine("\nЖурнал событий:");
            journal.PrintAllEvents();
        }

        static void EnterData(TransportCompany company)
        {
            company.AddTarif("Минск", 3.5);
            company.AddTarif("Гродно", 2.8);
            company.AddTarif("Брест", 4.1);

            Client client1 = new Client("Иванов", ClientType.VIP);
            Client client2 = new Client("Петров", ClientType.Regular);

            company.AddClient(client1);
            company.AddClient(client2);

            company.MakeOrder(client1, "Минск", 120);
            company.MakeOrder(client1, "Брест", 60);
            company.MakeOrder(client1, "Минск", 30);
            company.MakeOrder(client2, "Минск", 45.5);
            company.MakeOrder(client2, "Гродно", 200);
        }
    }
}
