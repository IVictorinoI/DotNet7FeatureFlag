using DotNet7FeatureFlag.App.Domain.Clients;
using DotNet7FeatureFlag.App.Repository.Clients;

namespace DotNet7FeatureFlag.App.App.Clients
{
    public class ClientApp : IClientApp
    {
        private readonly IRepClient _repClient;

        public ClientApp(IRepClient repClient)
        {
            _repClient = repClient;
        }

        public Client Add(ClientAddDto dto)
        {
            var sameValue = _repClient.GetByValue(dto.Value);
            if (sameValue != null)
            {
                throw new Exception($"Client '{dto.Value}' already exists.");
            }

            return _repClient.Add(new Client()
            {
                Description = dto.Description,
                Value = dto.Value
            });
        }

        public void RemoveAll()
        {
            _repClient.RemoveAll();
        }

        public int Count()
        {
            return _repClient.Count();
        }
    }
}
