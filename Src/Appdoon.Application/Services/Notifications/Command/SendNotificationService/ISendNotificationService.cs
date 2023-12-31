using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Mapdoon.Domain.Entities.Notification;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Notifications.Command.SendNotificationService
{
	public interface ISendNotificationService : ITransientService
	{
		Task<ResultDto<bool>> SendNotification(string message, int receiverId);
	}

	public class SendNotificationService : ISendNotificationService
	{
		private readonly IDatabaseContext _databaseContext;
		private readonly IWebSocketMessageSender _webSocketMessageSender;

		public string NotificationMethodName { get; } = "ReceiveNotification";

        public SendNotificationService(IDatabaseContext databaseContext, IWebSocketMessageSender webSocketMessageSender)
		{
			_databaseContext = databaseContext;
			_webSocketMessageSender = webSocketMessageSender;
		}
		public async Task<ResultDto<bool>> SendNotification(string message, int receiverId)
		{
			try
			{
				var notificaiton = new Notification
				{
					Message = message,
					ReceiverId = receiverId,
					IsSeen = false,
					InsertTime = DateTime.Now,
				};

				_databaseContext.Notifications.Add(notificaiton);
				await _databaseContext.SaveChangesAsync();

				_webSocketMessageSender.SendToUser(NotificationMethodName, receiverId.ToString(), new
				{
					NotificationId = notificaiton.Id,
					Message = message,
					Date = notificaiton.InsertTime,
				});

				return new ResultDto<bool>()
				{
					IsSuccess = true,
					Message = "Success",
				};
			}
			catch(Exception ex)
			{
				return new ResultDto<bool>()
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}
	}
}
