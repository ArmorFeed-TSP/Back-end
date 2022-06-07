using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Resources;

public class ShipmentResource
{
    public int Id { get; set; }
    public string OriginCity { get; set; }
    public string DestinationCity { get; set; }
    public DateTime PickUpDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int ShipmentStatus { get; set; }
    
    public Enterprise Enterprise { get; set; }
}