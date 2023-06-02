using ArmorFeedApi.Customers.Domain.Repositories;
using ArmorFeedApi.Enterprises.Domain.Repositories;
using ArmorFeedApi.Notifications.Domain.Models;
using ArmorFeedApi.Notifications.Domain.Repositories;
using ArmorFeedApi.Notifications.Domain.Services;
using ArmorFeedApi.Notifications.Domain.Services.Communication;
using ArmorFeedApi.Shared.Domain.Repositories;

namespace ArmorFeedApi.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly ICustomerRepository _customerRepository;

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IEnterpriseRepository enterpriseRepository, ICustomerRepository customerRepository)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _enterpriseRepository = enterpriseRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Notification>> getAllNotificationsByCustomerIdAsync(int customerId)
        {
            return await _notificationRepository.getAllNotificationsByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Notification>> getAllNotificationsByEnterpriseIdAsync(int enterpriseId)
        {
            return await _notificationRepository.getAllNotificationsByEnterpriseIdAsync(enterpriseId);
        }

        public async Task<NotificationResponse> saveAsync(Notification notification)
        {
            var existingEnterprise = await _enterpriseRepository.FindByIdAsync(notification.EnterpriseId);
            var existingCustomer = await _customerRepository.FindByIdAsync(notification.CustomerId);
            string errorResponse = string.Empty;
            if (existingEnterprise == null) {
                errorResponse += $"Enterprise with {notification.EnterpriseId} Id does not exist";
            }
            if (existingCustomer == null) {
                errorResponse += $"\nCustomer with {notification.CustomerId} Id does not Exist";
            }
            if(errorResponse.Equals(String.Empty) == false ) {
                return new NotificationResponse(errorResponse);
            }
            try
            {
                await _notificationRepository.addAsync(notification);
                await _unitOfWork.CompleteAsync();
                return new NotificationResponse(notification);
            }catch(Exception exception) {
                return new NotificationResponse($"An error occured while trying to save a notification: {exception.Message}");
            }
        }
    }
}
