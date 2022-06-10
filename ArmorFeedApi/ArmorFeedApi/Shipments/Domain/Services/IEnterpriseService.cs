

using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Services.Communication;

namespace ArmorFeedApi.Shipments.Domain.Services;

public interface IEnterpriseService
{
    Task<IEnumerable<Enterprise>> ListAsync();
    Task<EnterpriseResponse> SaveAsync(Enterprise enterprise);
    Task<EnterpriseResponse> UpdateAsync(int id, Enterprise enterprise);
    Task<EnterpriseResponse> DeleteAsync(int id);
}