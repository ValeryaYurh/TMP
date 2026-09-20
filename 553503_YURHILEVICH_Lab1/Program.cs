using _553503_YURHILEVICH_Lab1.Entities;
using _553503_YURHILEVICH_Lab1.Interfaces;

namespace _553503_YURHILEVICH_Lab1
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Вариант 7. Фирма грузоперевозок\n");

            TransportCompany company = new TransportCompany();
            SetupData(company);
           

            company.MakeOrder(client1, tarifMinsk, 120);
            company.MakeOrder(client1, tarifBrest, 60);
            company.MakeOrder(client2, tarifMinsk, 45.5);
            company.MakeOrder(client2, tarifGrodno, 200);

            Console.WriteLine("Список тарифов:");
            ICustomCollection<Tarif> tarifs = company.GetTarifs();
            for (int i = 0; i < tarifs.Count; i++)
                Console.WriteLine($"  {tarifs[i].Direction}: {tarifs[i].CostPerKg} руб/кг");

            Console.WriteLine("\nСписок клиентов и их заказов:");
            ICustomCollection<Client> clients = company.GetClients();
            for (int i = 0; i < clients.Count; i++)
            {
                Client client = clients[i];
                Console.WriteLine($"Клиент: {client.Name} ({client.Type})");

                ICustomCollection<Order> orders = client.GetOrders();
                for (int j = 0; j < orders.Count; j++)
                {
                    Order order = orders[j];
                    Console.WriteLine($"Заказ: направление \"{order.Tarif.Direction}\", вес {order.Weight} кг, стоимость {order.GetCost():F2}");
                }

                Console.WriteLine($"Сумма заказов клиента: {company.GetClientOrderSum(client):F2}");
            }

            Console.WriteLine($"\nОбщая стоимость всех заказов фирмы: {company.GetTotalOrdersSum():F2}");

            string direction = "Минск";
            Console.WriteLine($"Стоимость заказов на направление \"{direction}\": {company.GetSumByDirection(direction):F2}");

            string searchName = "Иванов";
            Client? found = company.FindClientByName(searchName);
            if (found != null)
                Console.WriteLine($"\nНайден клиент \"{found.Name}\", сумма его заказов: {company.GetClientOrderSum(found):F2}");
            else
                Console.WriteLine($"\nКлиент \"{searchName}\" не найден");
        }


        static void SetupData(TransportCompany company)
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
        }
    }


}
