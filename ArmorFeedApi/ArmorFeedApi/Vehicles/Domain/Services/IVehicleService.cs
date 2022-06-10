
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Services.Communication;

namespace ArmorFeedApi.Vehicles.Domain.Services;

public interface IVehicleService
{
    Task<IEnumerable<Vehicle>> ListAsync();
    Task<IEnumerable<Vehicle>> ListByEnterpriseIdAsync(int enterpriseId);
    Task<VehicleResponse> SaveAsync(Vehicle vehicle);
    Task<VehicleResponse> UpdateAsync(int vehicleId, Vehicle vehicle);
    Task<VehicleResponse> DeleteAsync(int vehicleId);
}