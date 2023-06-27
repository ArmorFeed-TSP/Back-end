using System.ComponentModel.DataAnnotations;
using ArmorFeedApi.Security.Domain.Services.Communication;

namespace ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;

public class RegisterShipmentDriverRequest : RegisterRequest
{
    [Required] public int EnterpriseId { get; set; }
}