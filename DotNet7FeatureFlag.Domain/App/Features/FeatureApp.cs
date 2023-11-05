using DotNet7FeatureFlag.App.App.Clients;
using DotNet7FeatureFlag.App.App.Features.Dtos;
using DotNet7FeatureFlag.App.Domain.Features;
using DotNet7FeatureFlag.App.Domain.Features.Profiles;
using DotNet7FeatureFlag.App.Domain.Features.Profiles.Clients;
using DotNet7FeatureFlag.App.Repository.Clients;
using DotNet7FeatureFlag.App.Repository.Features;

namespace DotNet7FeatureFlag.App.App.Features
{
    public class FeatureApp : IFeatureApp
    {
        private readonly IRepFeature _repFeature;
        private readonly IRepClient _repClient;
        private readonly IClientApp _clientApp;
        public FeatureApp(IRepFeature repFeature, IRepClient repClient, IClientApp clientApp)
        {
            _repFeature = repFeature;
            _repClient = repClient;
            _clientApp = clientApp;
        }

        public Feature Add(FeatureAddDto dto)
        {
            if (dto.ReleasedPerc < 0 || dto.ReleasedPerc > 100)
            {
                throw new Exception($"Relesead percent ({dto.ReleasedPerc}) out of range (0..100)");
            }

            var sameValue = _repFeature.GetByValue(dto.Value);
            if (sameValue != null)
            {
                throw new Exception($"Feature '{dto.Value}' already exists.");
            }

            return _repFeature.Add(new Feature()
            {
                Value = dto.Value,
                Description = dto.Description,
                ReleasedPerc = dto.ReleasedPerc,
                Profiles = new List<FeatureProfile>()
                {
                    new FeatureProfile()
                    {
                        Value = "Default",
                        Description = "Default",
                        ReleasedPerc = 100
                    }
                }
            });
        }

        public void RemoveAll()
        {
            _repFeature.RemoveAll();
        }

        public List<FeatureEnableDto> Check(string clientValue)
        {
            var client = _repClient.GetByValue(clientValue);
            if (client == null)
                _clientApp.Add(new ClientAddDto()
                {
                    Value = clientValue
                });

            var features = _repFeature.GetFeatures();

            var result = new List<FeatureEnableDto>();
            features.ForEach(feature =>
            {
                var profile = feature.GetProfile();
                result.Add(new FeatureEnableDto()
                {
                    FeatureValue = feature.Value,
                    ProfileValue = profile.Value,
                    Enable = profile.AllowedClient(clientValue)
                });
            });

            return result;
        }

        public FeatureEnableDto Check(FeatureCheckDto dto)
        {
            var client = _repClient.GetByValue(dto.ClientValue);
            if (client == null)
                client = _clientApp.Add(new ClientAddDto()
                {
                    Value = dto.ClientValue
                });

            var feature = _repFeature.GetByValue(dto.FeatureValue);
            var profile = feature.GetProfile();
            var totalClients = _repClient.Count();

            var alowedClientsCount = profile.Clients.Count;
            var expectedClients = totalClients * feature.ReleasedPerc / 100;
            if (alowedClientsCount < expectedClients)
            {
                AddClient(new FeatureProfileClientAddDto()
                {
                    ClientValue = client.Value,
                    FeatureValue = feature.Value,
                    ProfileValue = profile.Value
                });
            }

            
            var result = new FeatureEnableDto()
            {
                FeatureValue = feature.Value,
                ProfileValue = profile.Value,
                Enable = profile.AllowedClient(dto.ClientValue)
            };

            return result;
        }

        public FeatureProfileAllowedClient AddAllowedClient(FeatureProfileClientAddDto dto)
        {
            var feature = _repFeature.GetByValue(dto.FeatureValue);
            if (feature == null)
                throw new Exception($"Feature {dto.FeatureValue} not found");

            var profile = feature.Profiles.FirstOrDefault(p => p.Value == dto.ProfileValue);
            if (profile == null)
                throw new Exception($"Profile {dto.ProfileValue} not found");

            var client = _repClient.GetByValue(dto.ClientValue);
            if (client == null)
                throw new Exception($"Cliente {dto.ClientValue} not found");

            var profileClient = new FeatureProfileAllowedClient()
            {
                Client = client,
                FeatureProfile = profile,
                ClientId = client.Id,
                FeatureProfileId = profile.Id
            };
            profile.AllowedClients.Add(profileClient);

            return profileClient;
        }

        public FeatureProfileDaniedClient AddDaniedClient(FeatureProfileClientAddDto dto)
        {
            var feature = _repFeature.GetByValue(dto.FeatureValue);
            if (feature == null)
                throw new Exception($"Feature {dto.FeatureValue} not found");

            var profile = feature.Profiles.FirstOrDefault(p => p.Value == dto.ProfileValue);
            if (profile == null)
                throw new Exception($"Profile {dto.ProfileValue} not found");

            var client = _repClient.GetByValue(dto.ClientValue);
            if (client == null)
                throw new Exception($"Cliente {dto.ClientValue} not found");

            var profileClient = new FeatureProfileDaniedClient()
            {
                Client = client,
                FeatureProfile = profile,
                ClientId = client.Id,
                FeatureProfileId = profile.Id
            };
            profile.DaniedClients.Add(profileClient);

            return profileClient;
        }

        public FeatureProfileClient AddClient(FeatureProfileClientAddDto dto)
        {
            var feature = _repFeature.GetByValue(dto.FeatureValue);
            if (feature == null)
                throw new Exception($"Feature {dto.FeatureValue} not found");

            var profile = feature.Profiles.FirstOrDefault(p => p.Value == dto.ProfileValue);
            if (profile == null)
                throw new Exception($"Profile {dto.ProfileValue} not found");

            var client = _repClient.GetByValue(dto.ClientValue);
            if (client == null)
                throw new Exception($"Cliente {dto.ClientValue} not found");

            var profileClient = new FeatureProfileClient()
            {
                Client = client,
                FeatureProfile = profile,
                ClientId = client.Id,
                FeatureProfileId = profile.Id
            };
            profile.Clients.Add(profileClient);

            return profileClient;
        }
    }
}
