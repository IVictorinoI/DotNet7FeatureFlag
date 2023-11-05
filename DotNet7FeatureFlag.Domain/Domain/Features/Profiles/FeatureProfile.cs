using DotNet7FeatureFlag.App.Domain.Clients;
using DotNet7FeatureFlag.App.Domain.Features.Profiles.Clients;
using DotNet7FeatureFlag.Domain.Infra;

namespace DotNet7FeatureFlag.App.Domain.Features.Profiles
{
    public class FeatureProfile : Identificador
    {
        public FeatureProfile()
        {
            AllowedClients = new List<FeatureProfileAllowedClient>();
            DaniedClients = new List<FeatureProfileDaniedClient>();
            Clients = new List<FeatureProfileClient>();
        }

        public string Value { get; set; }
        public string Description { get; set; }
        public decimal ReleasedPerc { get; set; }
        public int FeatureId { get; set; }

        public Feature Feature { get; set; }
        public List<FeatureProfileAllowedClient> AllowedClients { get; set; }
        public List<FeatureProfileDaniedClient> DaniedClients { get; set; }
        public List<FeatureProfileClient> Clients { get; set; }

        public bool AllowedClient(string clientValue)
        {
            if (DaniedClients.Any(p => p.Client.Value == clientValue))
                return false;

            if (AllowedClients.Any(p => p.Client.Value == clientValue))
                return true;

            if (Clients.Any(p => p.Client.Value == clientValue))
                return true;

            return false;
        }
    }
}
