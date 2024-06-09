using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
	public class NotificationRequestDTO
	{
		public List<string> Recipients { get; set; } = null!;

		[Required(ErrorMessage = "Title can not be empty")]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = "Message text can not be empty")]
		public string Message { get; set; } = null!;

		public NotificationRequest ToNotificationRequest(string username)
		{
			return new NotificationRequest()
			{
				Sender = username,
				Recipients = Recipients,
				Title = Title,
				Message = Message
			};
		}
	}
}
