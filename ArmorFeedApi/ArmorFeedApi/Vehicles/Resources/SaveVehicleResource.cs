
using System.ComponentModel.DataAnnotations;

namespace ArmorFeedApi.Vehicles.Resources;

public class SaveVehicleResource
{
    [Required]
    [MaxLength(50)]
    public string Brand { get; set; }
    
    [MaxLength(120)]
    public string LicensePlate { get; set; }
    [MaxLength(3000)]
    public int Year { get; set; }
    [MaxLength(120)]
    public string Model { get; set; }
    [MaxLength(120)]
    public string MaintenanceDate { get; set; }
    [MaxLength(120)]
    public string VehicleType { get; set; }
    [Required]
    public int EnterpriseId { get; set; }
    
}