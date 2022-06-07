using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Resources;

public class ShipmentReviewResource
{
    public int Id { get; set; }
    public DateTime ReviewDate { get; set; }
    public string Text { get; set; }
    public int Score { get; set; }
    public Shipment Shipment { get; set; }
}