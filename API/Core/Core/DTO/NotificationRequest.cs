using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
	public class NotificationRequest
	{
		public string Sender { get; set; } = null!;

		public List<string> Recipients { get; set; } = null!;

		[Required(ErrorMessage = "Title can not be empty")]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = "Message text can not be empty")]
		public string Message { get; set; } = null!;
	}
}
