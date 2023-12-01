using Appdoon.Application.Services.Users.Command.LoginUserService;
using Mapdoon.Common.Interfaces;
using Mapdoon.Common.User;
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

		public JWTProvider(IOptions<JWTOptions> options)
		{
			_options = options.Value;
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

			return tokenValue;
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
