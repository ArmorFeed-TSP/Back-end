using ArmorFeedApi.Notifications.Domain.Models;
using ArmorFeedApi.Notifications.Domain.Repositories;
using ArmorFeedApi.Shared.Persistence.Contexts;
using ArmorFeedApi.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Notifications.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task addAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task<IEnumerable<Notification>> getAllNotificationsByCustomerIdAsync(int customerId)
        {
            return await _context.Notifications.Where(n => n.CustomerId == customerId && n.Sender.Equals(NotificationSender.ENTERPRISE)).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> getAllNotificationsByEnterpriseIdAsync(int enterpriseId)
        {
            return await _context.Notifications.Where(n => n.EnterpriseId == enterpriseId && n.Sender.Equals(NotificationSender.CUSTOMER)).ToListAsync();
        }
    }
}
