using Appdoon.Application.Services.Users.Command.LoginUserService;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Mapdoon.Common.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mapdoon.Application.Services.JWTAuthentication.Command
{
	public interface IJWTProvider : ITransientService
	{
		string Generate(UserLoginInfoDto userLoginInfoDto);
	}

	public class JWTProvider : IJWTProvider
	{
		private readonly JWTOptions _options;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserHubConnectionIdManager _userHubConnectionIdManager;

		public JWTProvider(IOptions<JWTOptions> options, IHttpContextAccessor httpContextAccessor, IUserHubConnectionIdManager userHubConnectionIdManager)
		{
			_options = options.Value;
			_httpContextAccessor = httpContextAccessor;
			_userHubConnectionIdManager = userHubConnectionIdManager;
		}
		public string Generate(UserLoginInfoDto userLoginInfoDto)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Name, userLoginInfoDto.Username),
				new Claim(JwtRegisteredClaimNames.Sub, userLoginInfoDto.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, userLoginInfoDto.Email),
				new Claim(nameof(UserRole), userLoginInfoDto.UserRole.ToString()),
			};

			var signingCredentials = new SigningCredentials(
								new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
								SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				_options.Issuer,
				_options.Audience,
				claims,
				null,
				DateTime.UtcNow.AddSeconds(_options.ExpirationSeocnds),
				signingCredentials);

			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

			SaveHubConnectionId(userLoginInfoDto);

			return tokenValue;
		}

		private void SaveHubConnectionId(UserLoginInfoDto userLoginInfoDto)
		{
			// get connectionId from headers
			var connectionId = _httpContextAccessor.HttpContext.Request?.Headers["connectionId"].ToString();

			if(string.IsNullOrEmpty(connectionId) == false)
			{
				_userHubConnectionIdManager.Add(userLoginInfoDto.Id.ToString(), connectionId);
			}
		}
	}

	public class JWTOptions
	{
		public string Issuer { get; init; }
		public string Audience { get; init; }
		public string SecretKey { get; init; }
		public int ExpirationSeocnds { get; set; }
	}
}
