using Mapdoon.Application.Services.Notification.Command.MarkNotificationAsRead;
using Mapdoon.Application.Services.Notification.Query.GetAllNotificationsService;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class NotificationController : Controller
	{
		private readonly ICurrentContext _currentContext;

		public NotificationController(ICurrentContext currentContext)
		{
			_currentContext = currentContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllNotifications([FromServices] IGetAllNotificationsService getAllNotificationsService, GetNotificationDto input)
		{
			var userId = _currentContext.User.Id;
			var result = await getAllNotificationsService.GetAllNotifications(input, userId);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> MarkAllNotificationsAsRead([FromServices] IMarkNotificationAsReadService markNotificationAsReadService)
		{
			var result = await markNotificationAsReadService.MarkAllNotificationsAsRead();
			return Ok(result);
		}

		[HttpPut]
		public async Task<IActionResult> MarkNotificationAsRead([FromServices] IMarkNotificationAsReadService markNotificationAsReadService, int notificationId)
		{
			var result = await markNotificationAsReadService.MarkNotificationAsRead(notificationId);
			return Ok(result);
		}
	}
}