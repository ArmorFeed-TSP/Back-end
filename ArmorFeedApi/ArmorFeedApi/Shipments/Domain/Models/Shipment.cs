
namespace ArmorFeedApi.Shipments.Domain.Models;

public class Shipment
{
    public int Id { get; set; }
    public string OriginCity { get; set; }
    public string DestinationCity { get; set; }
    public DateTime PickUpDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int ShipmentStatus { get; set; }
    
    public int EnterpriseId { get; set; }
    public Enterprise Enterprise { get; set; }
    
    public int CostumerId { get; set; }
    public Customer Customer { get; set; }
    
}