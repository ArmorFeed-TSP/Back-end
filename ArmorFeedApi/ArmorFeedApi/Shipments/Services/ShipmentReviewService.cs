using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Repositories;
using ArmorFeedApi.Shipments.Domain.Services;
using ArmorFeedApi.Shipments.Domain.Services.Communication;
using AutoMapper;

namespace ArmorFeedApi.Shipments.Services;

public class ShipmentReviewService: IShipmentService
{
    private readonly IShipmentReviewRepository _shipmentReviewRepository;
    private readonly IMapper _mapper;

    public ShipmentReviewService(IShipmentReviewRepository shipmentReviewRepository, IMapper mapper)
    {
        _shipmentReviewRepository = shipmentReviewRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Shipment>> ListAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<ShipmentResponse> SaveAsync(Shipment category)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipmentResponse> UpdateAsync(int id, Shipment category)
    {
        throw new NotImplementedException();
    }

    public async Task<ShipmentResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Shipment>> ListByEnterpriseId(int enterpriseId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Shipment>> ListByCustomerId(int customerId)
    {
        throw new NotImplementedException();
    }
}