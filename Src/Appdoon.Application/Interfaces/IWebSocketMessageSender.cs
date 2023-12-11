using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Interfaces
{
	public interface IWebSocketMessageSender : ITransientService
	{
		Task SendToAll(string methodName, object message);
		//Task SendToCurrentUser(string methodName, object message);
		Task SendToUser(string methodName, string userId, object message);
		Task SendToGroup(string methodName, string groupName, object message);
	}
}
