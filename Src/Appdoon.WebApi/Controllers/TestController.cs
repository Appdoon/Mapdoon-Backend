using Mapdoon.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mapdoon.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IWebSocketMessageSender _webSocketMessageSender;

		public TestController(IWebSocketMessageSender webSocketMessageSender)
		{
			_webSocketMessageSender = webSocketMessageSender;
		}

		[HttpPost]
		public IActionResult Post(string message)
		{
			_webSocketMessageSender.SendToAll("ReveiveMessage", message);
			return Ok();
		}
	}
}
