namespace ArmorFeedApi.Payments.Domain.Model;

public class Transaction
{
    public int Id { get; set; }
    public float Amount { get; set; }
    public string Currency { get; set; }
    public string PaymentDate { get; set; }
    public int ShipmentId { get; set; }
}