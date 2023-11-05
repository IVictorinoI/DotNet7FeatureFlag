using DotNet7FeatureFlag.App.Domain.Clients;
using DotNet7FeatureFlag.Domain.Infra;

namespace DotNet7FeatureFlag.App.Domain.Features.Profiles.Clients
{
    public class FeatureProfileClient : Identificador
    {
        public int ClientId { get; set; }
        public int FeatureProfileId { get; set; }

        public Client Client { get; set; }
        public FeatureProfile FeatureProfile { get; set; }
    }
}
