using DotNet7FeatureFlag.App.Domain.Features;

namespace DotNet7FeatureFlag.App.Repository.Features;

public interface IRepFeature
{
    Feature Add(Feature feature);
    void Remove(int id);
    void RemoveAll();
    Feature? GetByValue(string value);
    List<Feature> GetFeatures();
}