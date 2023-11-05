using DotNet7FeatureFlag.Domain.Infra;

namespace DotNet7FeatureFlag.App.Domain.Clients
{
    public class Client : Identificador
    {
        public Client()
        {
            LastAcess = DateTime.Now;
        }

        public string Value { get; set; }
        public string? Description { get; set; }
        public DateTime LastAcess { get; set; }
    }
}
