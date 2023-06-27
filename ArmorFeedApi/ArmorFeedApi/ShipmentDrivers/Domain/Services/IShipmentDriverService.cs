using ArmorFeedApi.Security.Domain.Services;
using ArmorFeedApi.Security.Domain.Services.Communication;
using ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;

namespace ArmorFeedApi.ShipmentDrivers.Domain.Services;

public interface IShipmentDriverService : IUserService<ShipmentDriver.Domain.Models.ShipmentDriver>
{
    Task<AuthenticateShipmentDriverResponse> Authenticate(AuthenticateRequest request);
    Task RegisterAsync(RegisterShipmentDriverRequest request);
    Task UpdateAsync(int id, UpdateShipmentDriverRequest request);
    Task<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>> GetAllByEnterpriseId(int enterpriseId);
}