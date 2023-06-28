using ArmorFeedApi.Customers.Domain.Services.Communication;
using ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;

namespace ArmorFeedApi.ShipmentDrivers.Mapping;

public class ResourceToModelProfile : AutoMapper.Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterShipmentDriverRequest, ShipmentDriver.Domain.Models.ShipmentDriver>();
        CreateMap<UpdateCustomerRequest, ShipmentDriver.Domain.Models.ShipmentDriver>()
            .ForAllMembers(options=>options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
            ));
        
    }
}