using ArmorFeedApi.Enterprises.Domain.Models;
using ArmorFeedApi.Security.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.ShipmentDriver.Domain.Models;

public class ShipmentDriver : User
{
    public Enterprise Enterprise { get; set; }
    public int EnterpriseId { get; set; }
    public IList<Shipment> Shipments { get; set; }
}