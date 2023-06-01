using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Enterprises.Domain.Models;

namespace ArmorFeedApi.Notifications.Domain.Models
{
    public enum NotificationSender
    {
        CUSTOMER = 0,
        ENTERPRISE
    }
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public NotificationSender Sender { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
    }
}
