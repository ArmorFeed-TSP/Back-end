

using ArmorFeedApi.Vehicles.Domain.Models;

namespace ArmorFeedApi.Vehicles.Domain.Repositories;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> ListAsync();
    Task AddAsync(Vehicle vehicle);
    Task<Vehicle> FindByIdAsync(int vehicleId);
    Task<Vehicle> FindByBrandAsync(string brand);
    Task<IEnumerable<Vehicle>> FindByEnterpriseIdAsync(int enterpriseId);
    void Update(Vehicle vehicle);
    void Remove(Vehicle vehicle);
}