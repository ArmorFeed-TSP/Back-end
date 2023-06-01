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
        [EnumDataType(typeof(NotificationSender))]
        public NotificationSender Sender { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int EnterpriseId { get; set; }
    }
}
