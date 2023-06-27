using ArmorFeedApi.Security.Domain.Respositories;

namespace ArmorFeedApi.ShipmentDrivers.Domain.Repositories;

public interface IShipmentDriverRepository : IUserRepository<ShipmentDriver.Domain.Models.ShipmentDriver>
{
    Task<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>> GetAllByEnterpriseId(int enterpriseId);
}