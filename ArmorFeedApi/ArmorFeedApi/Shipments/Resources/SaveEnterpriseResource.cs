


using System.ComponentModel.DataAnnotations;

namespace LearningCenter.API.Learning.Resources;

public class SaveEnterpriseResource
{
    [Required]
    public string Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}