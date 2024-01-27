using Mapdoon.Application.Interfaces;
using Mapdoon.Application.Services.Notifications.Command.SendNotificationService;
using Mapdoon.Common.Messages.Events;
using MassTransit;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Notifications.Consumer
{
	public class NotificationConsumer : IConsumer<SendNotificationEvent>
	{
		private readonly ISendNotificationService _sendNotificationService;
		private readonly IWebSocketMessageSender _webSocketMessageSender;

		public string NotificationMethodName { get; } = "ReceiveNotification";

		public NotificationConsumer(IWebSocketMessageSender webSocketMessageSender)
		{
			_webSocketMessageSender = webSocketMessageSender;
		}
		public async Task Consume(ConsumeContext<SendNotificationEvent> context)
		{
			var message = context.Message;

			_webSocketMessageSender.SendToUser(NotificationMethodName, message.ReceiverId.ToString(), new
			{
				NotificationId = message.NotificationId,
				Message = message.Notification,
				Date = message.SendTime,
			});

			return;
		}
	}
}
