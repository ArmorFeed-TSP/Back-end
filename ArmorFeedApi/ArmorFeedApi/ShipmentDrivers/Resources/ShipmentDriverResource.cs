using ArmorFeedApi.Security.Resources;

namespace ArmorFeedApi.ShipmentDrivers.Resources;

public class ShipmentDriverResource: UserResource
{
    public int EnterpriseId { get; set; }
}