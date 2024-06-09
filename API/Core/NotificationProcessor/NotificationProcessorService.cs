using Core.DTO;
using Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationProcessor
{
	public class NotificationProcessorService : BackgroundService
	{
		private readonly ConnectionFactory _connectionFactory;
		private readonly NotificationRepository _db;

		public NotificationProcessorService(ConnectionFactory connectionFactory, NotificationRepository notificationRepository)
		{
			_connectionFactory = connectionFactory;
			_connectionFactory.HostName = "localhost";
			_db = notificationRepository;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
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

				var consumer = new EventingBasicConsumer(channel);

				consumer.Received += async (sender, e) => await Consumer_ReceivedAsync(sender, e);

				channel.BasicConsume(
					queue: "q.emergency-notificator",
					autoAck: true,
					consumer: consumer);

				await Task.Delay(Timeout.Infinite, cancellationToken);
			}
		}

		private async Task Consumer_ReceivedAsync(object? sender, BasicDeliverEventArgs e)
		{
			var body = e.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			NotificationRequest? notificationRequest = JsonSerializer.Deserialize<NotificationRequest>(message);

			try
			{
				foreach (var recipient in notificationRequest.Recipients)
				{
					AddNotificationDTO addRequest = new AddNotificationDTO()
					{
						Title = notificationRequest.Title,
						Message = notificationRequest.Message,
						Sender = notificationRequest.Sender,
						Recipient = recipient
					};

					await _db.AddNotification(_db.ToNotification(addRequest));
					await Console.Out.WriteLineAsync("Added succesfully");
				}
			}
			catch (NullReferenceException ex)
			{
				Console.WriteLine($"NotificationRequest is null: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Something went wrong: {ex.Message}");
			}
		}
	}
}
