using System.ComponentModel.DataAnnotations;

namespace ArmorFeedApi.Shipments.Resources;

public class SaveShipmentResource
{
    [Required]
    [MaxLength(50)]
    public string OriginCity { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string DestinationCity { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime PickUpDate { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DeliveryDate { get; set; }
    
    [Required]
    public int ShipmentStatus { get; set; }
    
    [Required]
    public int EnterpriseId { get; set; }
}