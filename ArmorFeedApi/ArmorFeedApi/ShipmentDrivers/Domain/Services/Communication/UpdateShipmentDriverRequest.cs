using ArmorFeedApi.Security.Domain.Services.Communication;

namespace ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;

public class UpdateShipmentDriverRequest : UpdateRequest
{
    public string LastName { get; set; }
    public int EnterpriseId { get; set; }
}