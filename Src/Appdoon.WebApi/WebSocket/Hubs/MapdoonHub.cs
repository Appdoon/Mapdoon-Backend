using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.WebSocket.Hubs
{
	public class MapdoonHub : Hub
	{
		private readonly IUserHubConnectionIdManager _connectionIdManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICurrentContext _currentContext;

		public MapdoonHub(IUserHubConnectionIdManager connectionIdManager,
			IHttpContextAccessor httpContextAccessor,
			ICurrentContext currentContext)
		{
			_connectionIdManager = connectionIdManager;
			_httpContextAccessor = httpContextAccessor;
			_currentContext = currentContext;
		}
		public override Task OnConnectedAsync()
		{
			//var token = _httpContextAccessor.HttpContext.Request?.Headers["Authorization"].ToString().Replace("Bearer ", "");

			var user = Context.User;

			if(_currentContext.User.Id != 0)
				SaveUserHubConnectionId(Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			var context = Context.GetHttpContext();
			var userClaim = context.User.Claims
				.FirstOrDefault(x => x.Type == "sub");

			if(userClaim != null)
			{
				_connectionIdManager.Remove(userClaim.Value, Context.ConnectionId);
			}

			//var userId = _currentContext.User?.Id;
			//if(userId != null && userId != 0)
			//{
			//	_connectionIdManager.Remove(userId.ToString(), Context.ConnectionId);
			//}

			return base.OnDisconnectedAsync(exception);
		}

		private void SaveUserHubConnectionId(string connectionId)
		{
			if(string.IsNullOrWhiteSpace(connectionId) == false)
			{
				_connectionIdManager.Add(_currentContext.User.Id.ToString(), connectionId);
			}
		}
	}
}
