﻿namespace ArmorFeedApi.Customers.Resource;

public class CustomerResource
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Ruc { get; set; }
    public int SubscriptionPlan { get; set; }

}