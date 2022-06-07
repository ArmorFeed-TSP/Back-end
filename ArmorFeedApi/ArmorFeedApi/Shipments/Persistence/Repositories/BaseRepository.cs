using ArmorFeedApi.Shipments.Persistence.Contexts;

namespace ArmorFeedApi.Shipments.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}