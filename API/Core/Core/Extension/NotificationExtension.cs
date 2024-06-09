using Core.DTO;

namespace Core.Extension
{
	public static class NotificationExtension
	{
		public static GetNotificationDTO ToGetNotificationDTO(this Notification notification)
		{
			return new GetNotificationDTO
			{
				NotificationId = notification.NotificationId,
				Sender = notification.Sender.UserName,
				Recipient = notification.Recipient.UserName,
				Title = notification.Title,
				Message = notification.Message
			};
		}
	}
}
