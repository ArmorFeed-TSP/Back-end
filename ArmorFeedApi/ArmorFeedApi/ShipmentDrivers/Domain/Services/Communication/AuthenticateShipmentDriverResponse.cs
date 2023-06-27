using ArmorFeedApi.Security.Domain.Services.Communication;

namespace ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;

public class AuthenticateShipmentDriverResponse : AuthenticateResponse
{
    public string LastName { get; set; }
}