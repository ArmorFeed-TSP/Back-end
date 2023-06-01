using ArmorFeedApi.Notifications.Domain.Models;
using ArmorFeedApi.Shared.Domain.Services.Communication;

namespace ArmorFeedApi.Notifications.Domain.Services.Communication
{
    public class NotificationResponse : BaseResponse<Notification>
    {
        public NotificationResponse(Notification resource) : base(resource)
        {
        }

        public NotificationResponse(string message) : base(message)
        {
        }
    }
}
