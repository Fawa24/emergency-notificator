using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Identify_demo.Web.Controllers
{
	[ApiController]
	[Authorize]
	public class NotificationController : ControllerBase
	{
		private readonly NotificationService _notificationRequestService;

		public NotificationController(ConnectionFactory connectionFactory,
			NotificationService notificationRequestService)
		{
			_notificationRequestService = notificationRequestService;
		}

		[HttpPost("send")]
		public ActionResult SendNotification(NotificationRequestDTO notificationRequest)
		{
			string? sender = User?.Identity?.Name;

			if (sender == null) return BadRequest("Cannot set the sender for the notification");

			NotificationRequest notification = notificationRequest.ToNotificationRequest(sender);

			_notificationRequestService.SendNotificationRequest(notification);

			return Ok("Notification has been published");
		}

		[HttpGet("notifications")]
		public async Task<ActionResult<List<GetNotificationDTO>>> GetNotifications()
		{
			string username = User?.Identity?.Name;
			List<GetNotificationDTO> result = await _notificationRequestService.GetUserNotifications(username);

			return Ok(result);
		}
	}
}
