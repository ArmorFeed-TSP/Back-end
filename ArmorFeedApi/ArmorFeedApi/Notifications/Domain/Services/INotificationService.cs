using ArmorFeedApi.Notifications.Domain.Models;
using ArmorFeedApi.Notifications.Domain.Services.Communication;

namespace ArmorFeedApi.Notifications.Domain.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> getAllNotificationsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Notification>> getAllNotificationsByEnterpriseIdAsync(int enterpriseId);
        Task<NotificationResponse> saveAsync(Notification notification);
    }
}
