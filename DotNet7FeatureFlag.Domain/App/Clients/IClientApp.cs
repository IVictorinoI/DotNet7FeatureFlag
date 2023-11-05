using DotNet7FeatureFlag.App.Domain.Clients;

namespace DotNet7FeatureFlag.App.App.Clients;

public interface IClientApp
{
    Client Add(ClientAddDto dto);
    void RemoveAll();
    int Count();
}