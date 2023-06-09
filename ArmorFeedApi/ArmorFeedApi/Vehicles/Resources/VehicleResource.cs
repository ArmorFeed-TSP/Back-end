﻿
using ArmorFeedApi.Enterprises.Resources;
using ArmorFeedApi.Vehicles.Domain.Models;

namespace ArmorFeedApi.Vehicles.Resources;

public class VehicleResource
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string LicensePlate  { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string MaintenanceDate { get; set; }
    public string Image { get; set; }
    public VehicleState CurrentState { get; set; }
    public EnterpriseResource Enterprise;
}