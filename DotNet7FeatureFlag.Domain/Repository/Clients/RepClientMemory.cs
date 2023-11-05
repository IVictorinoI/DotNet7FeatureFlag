using DotNet7FeatureFlag.App.Domain.Clients;

namespace DotNet7FeatureFlag.App.Repository.Clients
{
    public class RepClientMemory : IRepClient
    {
        private static readonly List<Client> Clients = new List<Client>();

        public Client Add(Client client)
        {
            Clients.Add(client);
            return client;
        }

        public Client? GetByValue(string value)
        {
            return Clients.FirstOrDefault(p => p.Value == value);
        }

        public void Remove(int id)
        {
            var itemToRemove = Clients.FirstOrDefault(p => p.Id == id);
            Clients.Remove(itemToRemove ?? throw new InvalidOperationException($"Client {id} not found"));
        }

        public void RemoveAll()
        {
            Clients.Clear();
        }

        public int Count()
        {
            return Clients.Count;
        }
    }
}
