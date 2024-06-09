using Core.Domain.RepositoriesContracts;
using Core.DTO;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly ApplicationDbContext _db;

		public NotificationRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task AddNotification(Notification notification)
		{
			await _db.Notifications.AddAsync(notification);
			await _db.SaveChangesAsync();
		}

		public async Task<List<Notification>> GetUsersNotifications(string username)
		{
			return await _db.Notifications
				.Include(nameof(Notification.Sender))
				.Include(nameof(Notification.Recipient))
				.Where(n => n.Recipient.UserName.Equals(username))
				.ToListAsync();
		}

		public Notification ToNotification(AddNotificationDTO request)
		{
			return new Notification()
			{
				NotificationId = Guid.NewGuid(),
				Title = request.Title,
				Message = request.Message,
				SenderId = _db.Users.FirstOrDefault(user => user.UserName == request.Sender).Id,
				RecipientId = _db.Users.FirstOrDefault(user => user.UserName == request.Recipient).Id
			};
		}
	}
}
