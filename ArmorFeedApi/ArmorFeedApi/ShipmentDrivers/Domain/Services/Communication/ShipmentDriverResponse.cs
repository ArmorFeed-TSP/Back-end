using ArmorFeedApi.Shared.Domain.Services.Communication;

namespace ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;

public class ShipmentDriverResponse : BaseResponse<ShipmentDriver.Domain.Models.ShipmentDriver>
{
    public ShipmentDriverResponse(ShipmentDriver.Domain.Models.ShipmentDriver resource) : base(resource)
    {
    }

    public ShipmentDriverResponse(string message) : base(message)
    {
    }
}