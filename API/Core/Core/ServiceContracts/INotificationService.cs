using Core.DTO;

namespace Core.ServiceContracts
{
	/// <summary>
	/// NotificationService contract
	/// </summary>
	public interface INotificationService
	{
		/// <summary>
		/// Sends notification request to the NotificationProcessor through RabbitMQ
		/// </summary>
		/// <param name="request">Request to send</param>
		void SendNotificationRequest(NotificationRequest request);
		/// <summary>
		/// Returns notifications for a concrete user
		/// </summary>
		/// <param name="username">Username to search</param>
		/// <returns>All the notifications for provided user</returns>
		Task<List<GetNotificationDTO>> GetUserNotifications(string username);
	}
}
