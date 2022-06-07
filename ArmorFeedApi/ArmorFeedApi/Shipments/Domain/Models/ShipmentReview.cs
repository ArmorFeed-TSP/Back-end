namespace ArmorFeedApi.Shipments.Domain.Models;

public class ShipmentReview
{
    public int Id { get; set; }
    public DateTime ReviewDate { get; set; }
    public string Text { get; set; }
    public int Score { get; set; }
    
    public int ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
}