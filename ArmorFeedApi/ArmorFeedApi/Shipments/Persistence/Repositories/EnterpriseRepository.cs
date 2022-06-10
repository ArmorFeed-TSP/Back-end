

using ArmorFeedApi.Shared.Persistence.Contexts;
using ArmorFeedApi.Shared.Persistence.Repositories;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shipments.Persistence.Repositories;

public class EnterpriseRepository: BaseRepository, IEnterpriseRepository
{
    public EnterpriseRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Enterprise>> ListAsync()
    {
        return await _context.Enterprises.ToListAsync();
    }
    public async Task AddAsync(Enterprise enterprise)
    {
        await _context.Enterprises.AddAsync(enterprise);
    }

    public async Task<Enterprise> FindByIdAsync(int id)
    {
        return await _context.Enterprises.FindAsync(id);
    }

    public void Update(Enterprise enterprise)
    {
        _context.Enterprises.Update(enterprise);
    }

    public void Remove(Enterprise enterprise)
    {
        _context.Enterprises.Remove(enterprise);
    }
}