using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.WebSocket.Hubs
{
	[Authorize]
	public class MapdoonHub : Hub
	{
		private readonly IUserHubConnectionIdManager _connectionIdManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public MapdoonHub(IUserHubConnectionIdManager connectionIdManager,
			IHttpContextAccessor httpContextAccessor)
		{
			_connectionIdManager = connectionIdManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public override Task OnConnectedAsync()
		{
			//var token = _httpContextAccessor.HttpContext.Request?.Headers["Authorization"].ToString().Replace("Bearer ", "");

			var userId = Context.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

			if(userId != null)
				SaveUserHubConnectionId(Context.ConnectionId, userId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			var userId = Context.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

			if(userId != null)
			{
				_connectionIdManager.Remove(userId, Context.ConnectionId);
			}

			//var userId = _currentContext.User?.Id;
			//if(userId != null && userId != 0)
			//{
			//	_connectionIdManager.Remove(userId.ToString(), Context.ConnectionId);
			//}

			return base.OnDisconnectedAsync(exception);
		}

		private void SaveUserHubConnectionId(string connectionId, string userId)
		{
			if(string.IsNullOrWhiteSpace(connectionId) == false)
			{
				_connectionIdManager.Add(userId, connectionId);
			}
		}
	}
}
