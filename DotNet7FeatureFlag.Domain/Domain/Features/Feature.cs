using DotNet7FeatureFlag.App.Domain.Features.Profiles;
using DotNet7FeatureFlag.Domain.Infra;

namespace DotNet7FeatureFlag.App.Domain.Features
{
    public class Feature : Identificador
    {
        public Feature()
        {
            Profiles = new List<FeatureProfile>();
        }

        public string Value { get; set; }
        public string Description { get; set; }
        public decimal ReleasedPerc { get; set; }
        public List<FeatureProfile> Profiles { get; set; }

        public FeatureProfile GetProfile()
        {
            return Profiles.FirstOrDefault() ?? throw new InvalidOperationException("Profile not found");
        }
    }
}
