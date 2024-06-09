using Core.Domain.RepositoriesContracts;
using Core.DTO;
using Core.Extension;
using Core.ServiceContracts;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Core.Services
{
	public class NotificationService : INotificationService
	{
		private readonly ConnectionFactory _connectionFactory;
		private readonly INotificationRepository _notificationRepository;

		public NotificationService(ConnectionFactory connectionFactory,
			INotificationRepository notificationRepository)
		{
			_connectionFactory = connectionFactory;
			_notificationRepository = notificationRepository;
		}

		public async Task<List<GetNotificationDTO>> GetUserNotifications(string username)
		{
			return (await _notificationRepository.GetUsersNotifications(username))
				.Select(n => n.ToGetNotificationDTO())
				.ToList();
		}

		public void SendNotificationRequest(NotificationRequest request)
		{
			using (var connection = _connectionFactory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(
					queue: "q.emergency-notificator",
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				var message = JsonSerializer.Serialize(request);
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(
					exchange: "",
					routingKey: "q.emergency-notificator",
					body: body);
			}
		}
	}
}
