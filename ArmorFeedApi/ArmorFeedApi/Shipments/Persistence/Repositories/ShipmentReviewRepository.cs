

using ArmorFeedApi.Shared.Persistence.Contexts;
using ArmorFeedApi.Shared.Persistence.Repositories;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shipments.Persistence.Repositories;

public class ShipmentReviewRepository: BaseRepository, IShipmentReviewRepository
{
    public ShipmentReviewRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ShipmentReview>> ListAsync()
    {
        return await _context.ShipmentReviews.ToListAsync();
    }

    public async Task AddAsync(ShipmentReview shipmentReview)
    {
        await _context.ShipmentReviews.AddAsync(shipmentReview);
    }

    public async Task<ShipmentReview> FindByIdAsync(int id)
    {
        return await _context.ShipmentReviews.FindAsync(id);
    }

    public void Update(ShipmentReview shipmentReview)
    {
        _context.ShipmentReviews.Update(shipmentReview);
    }

    public void Remove(ShipmentReview shipmentReview)
    {
        _context.ShipmentReviews.Remove(shipmentReview);
    }

    public async Task<IEnumerable<ShipmentReview>> FindByShipmentId(int shipmentId)
    {
        return await _context.ShipmentReviews
            .Where(s => s.ShipmentId == shipmentId)
            .ToListAsync();
    }
}