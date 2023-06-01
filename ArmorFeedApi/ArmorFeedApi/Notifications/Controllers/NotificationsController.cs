using ArmorFeedApi.Notifications.Domain.Models;
using ArmorFeedApi.Notifications.Domain.Services;
using ArmorFeedApi.Notifications.Resources;
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace ArmorFeedApi.Notifications.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Create, Read, Update and Delete Vehicles")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet("customers/{customerId}")]
        [SwaggerOperation(
            Summary = "Get Notifications by Customer Id",
            Description = "Get Notifications by Customer Id",
            OperationId = "GetNotifications",
            Tags = new[] { "Notifications" }
        )]
        public async Task<IEnumerable<NotificationResource>> GetAllNotificationsByCustomerIdAsync(int customerId)
        {
            var result = await _notificationService.getAllNotificationsByCustomerIdAsync(customerId);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(result);
            return resources;
        }
        [HttpGet("enterprises/{enterpriseId}")]
        [SwaggerOperation(
            Summary = "Get Notifications by Enterprise Id",
            Description = "Get Notifications by Enterprise Id",
            OperationId = "GetNotifications",
            Tags = new[] { "Notifications" }
        )]
        public async Task<IEnumerable<NotificationResource>> GetAllNotificationsByEnterpriseId(int enterpriseId)
        {
            var result = await _notificationService.getAllNotificationsByEnterpriseIdAsync(enterpriseId);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(result);
            return resources;
        }
        [HttpPost]
        [SwaggerOperation(
            Summary = "Post Notification",
            Description = "Save Notification In Database",
            OperationId = "PostNotification",
            Tags = new[] { "Notification" }
        )]
        public async Task<IActionResult> PostAsync([FromBody] SaveNotificationResource saveNotificationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var newNotification = _mapper.Map<SaveNotificationResource, Notification>(saveNotificationResource);
            var result = await _notificationService.saveAsync(newNotification);
            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
            return Ok(notificationResource);
        }
    }
}
