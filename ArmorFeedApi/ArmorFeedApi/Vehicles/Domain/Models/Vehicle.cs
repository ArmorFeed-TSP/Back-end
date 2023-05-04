

using ArmorFeedApi.Enterprises.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Models;
using System.Runtime.Versioning;

namespace ArmorFeedApi.Vehicles.Domain.Models;

public enum VehicleState
{
    AVAILABLE,
    OCCUPIED,
    IN_MAINTENANCE
}

public class Vehicle
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string LicensePlate  { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string MaintenanceDate { get; set; }
    public VehicleState CurrentState { get; set; }
    
    //Relationships
    public int EnterpriseId { get; set; }
    public Enterprise Enterprise { get; set; }
    public Shipment Shipment { get; set; }

}