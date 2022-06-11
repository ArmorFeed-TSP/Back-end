using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Repositories;
using ArmorFeedApi.Shipments.Domain.Services;
using ArmorFeedApi.Shipments.Domain.Services.Communication;
using AutoMapper;

namespace ArmorFeedApi.Shipments.Services;

public class ShipmentReviewService: IShipmentReviewService
{
    private readonly IShipmentReviewRepository _shipmentReviewRepository;
    private readonly IMapper _mapper;

    public ShipmentReviewService(IShipmentReviewRepository shipmentReviewRepository, IMapper mapper)
    {
        _shipmentReviewRepository = shipmentReviewRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShipmentReview>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ShipmentReviewResponse> SaveAsync(ShipmentReview category)
    {
        throw new NotImplementedException();
    }

    public Task<ShipmentReviewResponse> UpdateAsync(int id, ShipmentReview category)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipmentReviewResponse> SaveAsync(Shipment category)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipmentReviewResponse> UpdateAsync(int id, Shipment category)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipmentReviewResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ShipmentReview>> ListByShipmentId(int shipmentId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ShipmentReview>> ListByEnterpriseId(int enterpriseId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ShipmentReview>> ListByCustomerId(int customerId)
    {
        throw new NotImplementedException();
    }
}