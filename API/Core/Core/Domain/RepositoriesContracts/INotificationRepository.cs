﻿using Core.DTO;

namespace Core.Domain.RepositoriesContracts
{
	/// <summary>
	/// NotificationRepository contract
	/// </summary>
	public interface INotificationRepository
	{
		/// <summary>
		/// Adds new notification to the data source
		/// </summary>
		/// <param name="notification">Notification to add</param>
		/// <returns></returns>
		Task AddNotification(Notification notification);
		/// <summary>
		/// Returns notifications for a concrete user
		/// </summary>
		/// <param name="username">Username to search</param>
		/// <returns>All the notifications for provided user</returns>
		Task<List<Notification>> GetUsersNotifications(string username);
		/// <summary>
		/// Converts AddNotificationDTO request into Notification entity
		/// </summary>
		/// <param name="request">Request to convert</param>
		/// <returns>Notification object</returns>
		Notification ToNotification(AddNotificationDTO request);
	}
}
