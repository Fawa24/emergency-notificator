namespace Core.DTO
{
	public class GetNotificationDTO
	{
		public Guid NotificationId { get; set; }
		public string Sender { get; set; } = null!;
		public string Recipient { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Message { get; set; } = null!;
	}
}
