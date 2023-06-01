using ArmorFeedApi.Notifications.Domain.Models;

namespace ArmorFeedApi.Notifications.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> getAllNotificationsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Notification>> getAllNotificationsByEnterpriseIdAsync(int enterpriseId);
        Task addAsync(Notification notification);
    }
}
