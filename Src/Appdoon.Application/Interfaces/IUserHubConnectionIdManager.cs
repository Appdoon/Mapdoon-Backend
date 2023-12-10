using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Interfaces
{
	public interface IUserHubConnectionIdManager : ITransientService
	{
		void Add(string key, string connectionId);
		IEnumerable<string> GetConnections(string key);
		void Remove(string key, string connectionId);
	}
}
