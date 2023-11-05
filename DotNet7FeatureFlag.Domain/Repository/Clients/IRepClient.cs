using DotNet7FeatureFlag.App.Domain.Clients;

namespace DotNet7FeatureFlag.App.Repository.Clients;

public interface IRepClient
{
    Client Add(Client client);
    void Remove(int id);
    void RemoveAll();
    int Count();
    Client? GetByValue(string value);
}