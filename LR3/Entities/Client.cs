using System.Collections.Generic;

namespace LR3.Entities
{
    public enum ClientType
    {
        Regular,
        VIP
    }

    public class Client
    {
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        public Client(string name, ClientType type)
        {
            Name = name;
            Type = type;
        }
    }
}
