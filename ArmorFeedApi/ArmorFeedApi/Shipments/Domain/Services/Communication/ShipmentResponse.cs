using ArmorFeedApi.Shared.Domain.Services.Communication;
using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Domain.Services.Communication;

public class ShipmentResponse: BaseResponse<Shipment>
{
    public ShipmentResponse(Shipment resource) : base(resource)
    {
    }

    public ShipmentResponse(string message) : base(message)
    {
    }
}