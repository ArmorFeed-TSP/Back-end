namespace ArmorFeedApi.Customers.Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Ruc { get; set; }
    public int SubscriptionPlan { get; set; }
}