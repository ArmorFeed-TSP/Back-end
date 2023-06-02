using ArmorFeedApi.Notifications.Domain.Models;

namespace ArmorFeedApi.Notifications.Resources
{
    public class NotificationResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public NotificationSender Sender { get; set; }
        public int CustomerId { get; set; }
        public int EnterpriseId { get; set; }
    }
}
