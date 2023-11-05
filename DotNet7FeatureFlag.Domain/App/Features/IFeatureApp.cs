using DotNet7FeatureFlag.App.App.Features.Dtos;
using DotNet7FeatureFlag.App.Domain.Features;
using DotNet7FeatureFlag.App.Domain.Features.Profiles.Clients;

namespace DotNet7FeatureFlag.App.App.Features;

public interface IFeatureApp
{
    Feature Add(FeatureAddDto dto);
    void RemoveAll();
    FeatureProfileAllowedClient AddAllowedClient(FeatureProfileClientAddDto dto);
    FeatureProfileDaniedClient AddDaniedClient(FeatureProfileClientAddDto dto);
    FeatureProfileClient AddClient(FeatureProfileClientAddDto dto);
    FeatureEnableDto Check(FeatureCheckDto dto);
    List<FeatureEnableDto> Check(string clientValue);
}