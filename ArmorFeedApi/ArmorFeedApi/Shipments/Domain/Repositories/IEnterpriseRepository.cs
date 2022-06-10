

using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Domain.Repositories;

public interface IEnterpriseRepository
{
    Task<IEnumerable<Enterprise>> ListAsync();
    Task AddAsync(Enterprise enterprise);
    Task<Enterprise> FindByIdAsync(int id);
    void Update(Enterprise enterprise);
    void Remove(Enterprise enterprise);
}