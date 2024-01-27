using Mapdoon.Application.Interfaces;
using Mapdoon.Application.Services.Notifications.Command.SendNotificationService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IWebSocketMessageSender _webSocketMessageSender;
		private readonly ISendNotificationService _sendNotificationService;

		public TestController(IWebSocketMessageSender webSocketMessageSender, ISendNotificationService sendNotificationService)
		{
			_webSocketMessageSender = webSocketMessageSender;
			_sendNotificationService= sendNotificationService;
		}

		[HttpPost]
		public IActionResult ChatMessage(string message)
		{
			_webSocketMessageSender.SendToAll("ReceiveMessage", message);
			return Ok();
		}

		[HttpPost]
		public IActionResult NotificationMessage(string message)
		{
			_webSocketMessageSender.SendToAll("ReceiveNotification", message);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> CreateNotification(string message, int receiverId)
		{
			var result = await _sendNotificationService.SendNotification(message, receiverId);

			return Ok(result);
		}
	}
}
