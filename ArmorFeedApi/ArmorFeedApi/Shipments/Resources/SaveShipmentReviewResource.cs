using System.ComponentModel.DataAnnotations;
using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Resources;

public class SaveShipmentReviewResource
{
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ReviewDate { get; set; }
    
    [Required]
    [MinLength(20)]
    public string Text { get; set; }
    
    [Required]
    public int Score { get; set; }
    
    [Required]
    public Shipment Shipment { get; set; }
}