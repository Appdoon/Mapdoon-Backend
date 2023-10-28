// Ignore Spelling: Username

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Mapdoon.Common.User;

public class MapdoonUser
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public UserRole UserRole { get; set; }

	public static MapdoonUser GetUser(List<Claim> claims)
	{
		try
		{
			var id = int.Parse(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
			var username = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value;
			var email = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
			var role = Enum.Parse<UserRole>(claims.FirstOrDefault(x => x.Type == nameof(UserRole)).Value);

			var user = new MapdoonUser()
			{
				Id = id,
				Username = username,
				Email = email,
				UserRole = role,
			};

			return user;
		}
		catch
		{
			return new MapdoonUser();
		}
	}
}
