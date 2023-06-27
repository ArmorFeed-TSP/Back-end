using ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;
using ArmorFeedApi.ShipmentDrivers.Resources;

namespace ArmorFeedApi.ShipmentDrivers.Mapping;

public class ModelToResourceProfile : AutoMapper.Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<ShipmentDriver.Domain.Models.ShipmentDriver, AuthenticateShipmentDriverResponse>();
        CreateMap<ShipmentDriver.Domain.Models.ShipmentDriver, ShipmentDriverResponse>();
        CreateMap<ShipmentDriver.Domain.Models.ShipmentDriver, ShipmentDriverResource>();
    }
}