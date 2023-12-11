using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Mapdoon.WebApi.WebSocket.Hubs
{
	public class MapdoonHub : Hub
	{
		private readonly IUserHubConnectionIdManager _connectionIdManager;
		private readonly ICurrentContext _currentContext;

		public MapdoonHub(IUserHubConnectionIdManager connectionIdManager,
			ICurrentContext currentContext)
		{
			_connectionIdManager = connectionIdManager;
			_currentContext = currentContext;
		}
		public override Task OnConnectedAsync()
		{
			if(_currentContext.User.Id != 0)
				SaveUserHubConnectionId(Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			var context = Context.GetHttpContext();
			var userClaim = context.User.Claims
				.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

			if(userClaim != null)
			{
				_connectionIdManager.Remove(userClaim.Value, Context.ConnectionId);
			}
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
