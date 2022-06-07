using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Services.Communication;

namespace ArmorFeedApi.Shipments.Domain.Services;

public interface IShipmentReviewService
{
    Task<IEnumerable<ShipmentReview>> ListAsync();
    Task<ShipmentReviewResponse> SaveAsync(ShipmentReview category);
    Task<ShipmentReviewResponse> UpdateAsync(int id, ShipmentReview category);
    Task<ShipmentReviewResponse> DeleteAsync(int id);
    Task<IEnumerable<ShipmentReview>> ListByShipmentId(int shipmentId);
}