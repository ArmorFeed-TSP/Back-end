using ArmorFeedApi.Shared.Persistence.Contexts;
using ArmorFeedApi.Shared.Persistence.Repositories;
using ArmorFeedApi.ShipmentDrivers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.ShipmentDrivers.Persistence.Repository;

public class ShipmentDriverRepository : BaseRepository, IShipmentDriverRepository
{
    public ShipmentDriverRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>> ListAsync()
    {
        return await _context.ShipmentDrivers.ToListAsync();
    }

    public async Task AddAsync(ShipmentDriver.Domain.Models.ShipmentDriver? newShipmentDriver)
    {
        await _context.ShipmentDrivers.AddAsync(newShipmentDriver);
    }

    public async Task<ShipmentDriver.Domain.Models.ShipmentDriver> FindByIdAsync(int id)
    {
        return await _context.ShipmentDrivers.FindAsync(id);
    }

    public async Task<ShipmentDriver.Domain.Models.ShipmentDriver> FindByEmailAsync(string email)
    {
        return await _context.ShipmentDrivers.FirstOrDefaultAsync(driver => driver.Email == email);
    }

    public bool ExitsByEmail(string email)
    {
        return _context.Enterprises.Any(x => x.Email == email);
    }

    public ShipmentDriver.Domain.Models.ShipmentDriver FindById(int id)
    {
        return _context.ShipmentDrivers.Find(id);
    }

    public void Update(ShipmentDriver.Domain.Models.ShipmentDriver user)
    {
        _context.Update(user);
    }

    public void Remove(ShipmentDriver.Domain.Models.ShipmentDriver user)
    {
        _context.Remove(user);
    }

    public async Task<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>> GetAllByEnterpriseId(int enterpriseId)
    {
        return await _context.ShipmentDrivers
            .Where(sd => sd.EnterpriseId == enterpriseId)
            .ToListAsync();
    }
}