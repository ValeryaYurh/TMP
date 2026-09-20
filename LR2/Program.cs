using LR2.Entities;
using LR2.Exceptions;
using LR2.Interfaces;

namespace LR2
{
    internal class Program
    {
        static void Main()
        {
            TransportCompany company = new TransportCompany();
            Journal journal = new Journal();

            company.DataChanged += (sender, e) => journal.LogEvent("TransportCompany", e.Description);

            company.OrderPlaced += (sender, e) =>
                Console.WriteLine($"[Событие] Клиент \"{e.ClientName}\" заказал перевозку: " +
                                   $"направление \"{e.Direction}\", вес {e.Weight} кг, стоимость {e.Cost:F2} руб.");

            EnterData(company);


            Console.WriteLine("\nВариант 7. Фирма грузоперевозок\n");

            Console.WriteLine("Список тарифов:");
            foreach (Tarif tarif in company.GetTarifs())
                Console.WriteLine($"  {tarif.Direction}: {tarif.CostPerKg} руб/кг");

            Console.WriteLine("\nСписок клиентов и их заказов:");
            foreach (Client client in company.GetClients())
            {
                Console.WriteLine($"  Клиент: {client.Name} ({client.Type})");

                foreach (Order order in client.GetOrders())
                    Console.WriteLine($"    Заказ: направление \"{order.Tarif.Direction}\", " +
                                       $"вес {order.Weight} кг, стоимость {order.GetCost():F2}");

                Console.WriteLine($"  Сумма заказов клиента: {company.GetClientOrderSum(client):F2}");
            }

            Console.WriteLine($"\nОбщая стоимость всех заказов фирмы: {company.GetTotalOrdersSum():F2}");

            string direction = "Минск";
            Console.WriteLine($"Стоимость заказов на направление \"{direction}\": {company.GetSumByDirection(direction):F2}");

            Console.WriteLine("\nДемонстрация обработки исключений:");

            ICustomCollection<Tarif> tarifs = company.GetTarifs();

            try
            {
                Tarif bad = tarifs[100]; 
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"  Поймано IndexOutOfRangeException: {ex.Message}");
            }

            try
            {
                Tarif unknown = new Tarif("Неизвестное направление", 1);
                tarifs.Remove(unknown); 
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"  Поймано ItemNotFoundException: {ex.Message}");
            }

            Console.WriteLine("\nЖурнал событий:");
            journal.PrintAllEvents();
        }

        static void EnterData(TransportCompany company)
        {
            Tarif tarifMinsk = new Tarif("Минск", 3.5);
            Tarif tarifGrodno = new Tarif("Гродно", 2.8);
            Tarif tarifBrest = new Tarif("Брест", 4.1);

            company.AddTarif(tarifMinsk);
            company.AddTarif(tarifGrodno);
            company.AddTarif(tarifBrest);

            Client client1 = new Client("Иванов", ClientType.VIP);
            Client client2 = new Client("Петров", ClientType.Regular);

            company.AddClient(client1);
            company.AddClient(client2);

            company.MakeOrder(client1, tarifMinsk, 120);
            company.MakeOrder(client1, tarifBrest, 60);
            company.MakeOrder(client2, tarifMinsk, 45.5);
            company.MakeOrder(client2, tarifGrodno, 200);
        }
    }
}
