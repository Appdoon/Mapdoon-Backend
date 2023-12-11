using Mapdoon.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Mapdoon.Common.HubManager
{
	public class UserHubConnectionIdManager : IUserHubConnectionIdManager
	{
		private readonly IMemoryCache _cacheProvider;
		private const string _keyPrefix = "HubConnectionId";

		public UserHubConnectionIdManager(IMemoryCache cacheProvider)
		{
			_cacheProvider = cacheProvider;
		}

		public void Add(string key, string connectionId)
		{
			key = _keyPrefix + "_" + key;
			var value = _cacheProvider.Get<List<string>>(key);
			if(value == null)
			{
				value = new List<string>() { connectionId };
			}
			else
			{
				value.Add(connectionId);
			}

			_cacheProvider.Set(key, value);
		}

		public IEnumerable<string> GetConnections(string key)
		{
			key = _keyPrefix + "_" + key;
			var connectionIds = _cacheProvider.Get<List<string>>(key);
			return connectionIds;
		}

		public void Remove(string key, string connectionId)
		{
			key = _keyPrefix + "_" + key;
			var value = _cacheProvider.Get<List<string>>(key);
			if(value != null)
			{
				value.Remove(connectionId);
				_cacheProvider.Set(key, value);
			}
		}
	}
}
