using Mapdoon.Common.User;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Mapdoon.Common.Interfaces
{
	public interface ICurrentContext : IScopedService
	{
		public MapdoonUser User { get; }
	}

	public class CurrentContext : ICurrentContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public CurrentContext(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;

			var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Really??
            //if (string.IsNullOrWhiteSpace(token) == true)
            //    return null;

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var payload = jwtToken.Payload;

			var claims = jwtToken.Payload.Claims.ToList();

			_user = MapdoonUser.GetUser(claims);
		}

		private readonly MapdoonUser _user;

		public MapdoonUser User
		{
			get
			{
				return _user;
			}
		}
	}
}
