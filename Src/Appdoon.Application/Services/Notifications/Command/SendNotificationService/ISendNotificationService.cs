using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Mapdoon.Common.Messages.Events;
using Mapdoon.Domain.Entities.Notification;
using MassTransit;
using System;
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
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly IWebSocketMessageSender _webSocketMessageSender;

		public string NotificationMethodName { get; } = "ReceiveNotification";

		public SendNotificationService(IDatabaseContext databaseContext, IPublishEndpoint publishEndpoint)
		{
			_databaseContext = databaseContext;
			_publishEndpoint = publishEndpoint;
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

				await _publishEndpoint.Publish(new SendNotificationEvent
				{
					NotificationId = notificaiton.Id,
					Notification = notificaiton.Message,
					ReceiverId = notificaiton.ReceiverId,
					SendTime = notificaiton.InsertTime,
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
