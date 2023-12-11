using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Mapdoon.WebApi.WebSocket.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Mapdoon.WebApi.WebSocket
{
	public class WebSocketMessageSender : IWebSocketMessageSender
	{
		private readonly IHubContext<MapdoonHub> _mapdoonHub;
		private readonly IUserHubConnectionIdManager _connectionIdManager;
		private readonly ICurrentContext _currentContext;

		public WebSocketMessageSender(IHubContext<MapdoonHub> mapdoonHub,
			IUserHubConnectionIdManager connectionIdManager,
			ICurrentContext currentContext)
		{
			_mapdoonHub = mapdoonHub;
			_connectionIdManager = connectionIdManager;
			_currentContext = currentContext;
		}
		public async Task SendToAll(string methodName, object message)
		{
			await _mapdoonHub.Clients.All.SendAsync(methodName, message);
		}

		//public async Task SendToCurrentUser(string methodName, object message)
		//{
		//	await _mapdoonHub.Clients.Client(_currentContext.HubConnectionId).SendAsync(methodName, message);
		//}
		public async Task SendToUser(string methodName, string userId, object message)
		{
			var connections = _connectionIdManager.GetConnections(userId);
			if(connections == null)
				return;
			foreach(var hubConnectionId in connections)
			{
				if(hubConnectionId == null)
					continue;
				await _mapdoonHub.Clients.Client(hubConnectionId).SendAsync(methodName, message);
			}
		}

		public async Task SendToGroup(string methodName, string groupName, object message)
		{
			await _mapdoonHub.Clients.Group(groupName).SendAsync(methodName, message);
		}

	}
}
