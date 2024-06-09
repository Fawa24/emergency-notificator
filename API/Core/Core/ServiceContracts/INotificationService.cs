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
	}
}
