

using ArmorFeedApi.Shared.Persistence.Contexts;
using ArmorFeedApi.Shared.Persistence.Repositories;
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Vehicles.Persistence.Repositories;

public class VehicleRepository: BaseRepository, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Vehicle>> ListAsync()
    {
        return await _context.Vehicles.ToListAsync();

    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
    }

    public async Task<Vehicle> FindByIdAsync(int vehicleId)
    {
        return await _context.Vehicles.FindAsync(vehicleId);
    }

    public async Task<Vehicle> FindByBrandAsync(string licensePlate)
    {
        return await _context.Vehicles
            .Include(p => p.Enterprise)
            .FirstOrDefaultAsync(p => p.LicensePlate == licensePlate);
    }
    
    public async Task<IEnumerable<Vehicle>> FindByEnterpriseIdAsync(int enterpriseId)
    {
        return await _context.Vehicles
            .Where(p => p.EnterpriseId == enterpriseId)
            .Include(p => p.Enterprise)
            .ToListAsync();
    }
    
    public void Update(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
    }

    public void Remove(Vehicle vehicle)
    {
        _context.Vehicles.Remove(vehicle);
    }
}