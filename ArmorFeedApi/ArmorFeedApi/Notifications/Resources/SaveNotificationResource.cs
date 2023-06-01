using ArmorFeedApi.Notifications.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ArmorFeedApi.Notifications.Resources
{
    public class SaveNotificationResource
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(NotificationSender), ErrorMessage = "Notification Sender not valid")]
        public NotificationSender Sender { get; set; }

        [Required(ErrorMessage = "Customer Id is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Enterprise Id is required")]
        public int EnterpriseId { get; set; }
    }
}
