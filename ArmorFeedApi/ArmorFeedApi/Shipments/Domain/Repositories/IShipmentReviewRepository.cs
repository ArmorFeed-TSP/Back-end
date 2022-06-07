using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Domain.Repositories;

public interface IShipmentReviewRepository
{
    Task<IEnumerable<ShipmentReview>> ListAsync();
    Task AddAsync(ShipmentReview shipment);
    Task<ShipmentReview> FindByIdAsync(int id);
    void Update(ShipmentReview shipment);
    void Remove(ShipmentReview shipment);
    Task<IEnumerable<ShipmentReview>> FindByShipmentId(int shipmentId);
}