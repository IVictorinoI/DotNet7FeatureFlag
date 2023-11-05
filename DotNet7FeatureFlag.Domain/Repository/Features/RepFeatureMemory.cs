using DotNet7FeatureFlag.App.Domain.Features;

namespace DotNet7FeatureFlag.App.Repository.Features
{
    public class RepFeatureMemory : IRepFeature
    {
        private static readonly List<Feature> Features = new List<Feature>();

        public Feature Add(Feature feature)
        {
            Features.Add(feature);
            return feature;
        }

        public Feature? GetByValue(string value)
        {
            return Features.FirstOrDefault(p => p.Value == value);
        }

        public void Remove(int id)
        {
            var itemToRemove = Features.FirstOrDefault(p => p.Id == id);
            Features.Remove(itemToRemove ?? throw new InvalidOperationException($"Feature {id} not found"));
        }

        public void RemoveAll()
        {
            Features.Clear();
        }

        public List<Feature> GetFeatures()
        {
            return Features;
        }
    }
}
