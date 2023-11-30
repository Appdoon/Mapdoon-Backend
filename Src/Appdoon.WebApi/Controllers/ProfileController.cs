﻿using Appdoon.Application.Services.Users.Command.EditPasswordService;
using Appdoon.Application.Services.Users.Command.EditUserService;
using Appdoon.Application.Services.Users.Query.GetBookMarkRoadMapService;
using Appdoon.Application.Services.Users.Query.GetCreatedLessonsService;
using Appdoon.Application.Services.Users.Query.GetCreatedRoadMapService;
using Appdoon.Application.Services.Users.Query.GetRegisteredRoadMapService;
using Appdoon.Application.Services.Users.Query.GetUserService;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appdoon.WebApi.Controllers
{
	[Authorize(policy: "User")]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProfileController : Controller
	{
		private readonly IGetUserService _getUserService;
		private readonly IEditUserService _editUserService;
		private readonly IGetRegisteredRoadMapService _getRegisteredRoadMapService;
		private readonly IGetBookMarkRoadMapService _getBookMarkRoadMapService;

		private readonly IEditPasswordService _editPasswordService;
		private readonly IGetCreatedRoadMapService _getCreatedRoadMapService;
		private readonly IGetCreatedLessonsService _getCreatedLessonsService;
		private readonly ICurrentContext _currentContext;

		public ProfileController(IGetUserService getUserService,
								 IEditUserService editUserService,
								 IGetRegisteredRoadMapService getRegisteredRoadMapService,
								 IGetBookMarkRoadMapService getBookMarkRoadMapService,
								 IEditPasswordService editPasswordService,
								 IGetCreatedRoadMapService getCreatedRoadMapService,
								 IGetCreatedLessonsService getCreatedLessonsService,
								 ICurrentContext currentContext)
		{
			_getUserService = getUserService;
			_editUserService = editUserService;
			_getRegisteredRoadMapService = getRegisteredRoadMapService;
			_getBookMarkRoadMapService = getBookMarkRoadMapService;
			_editPasswordService = editPasswordService;
			_getCreatedRoadMapService = getCreatedRoadMapService;
			_getCreatedLessonsService = getCreatedLessonsService;
			_currentContext = currentContext;
		}
		[HttpGet]
		public IActionResult Info()
		{
			int Id = GetIdFromCookie();
			var result = _getUserService.Execute(Id);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult Edit(EditUserDto UserDto)
		{
			int Id = GetIdFromCookie();
			var result = _editUserService.Execute(Id, UserDto);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult EditPassword(EditPasswordDto PasswordDto)
		{
			int Id = GetIdFromCookie();
			var result = _editPasswordService.Execute(Id, PasswordDto);
			return Ok(result);
		}

		[HttpGet]
		public IActionResult RegisteredRoadMaps()
		{
			int Id = GetIdFromCookie();

			var result = _getRegisteredRoadMapService.Execute(Id);

			return Ok(result);
		}

		[HttpGet]
		public IActionResult BookMarkedRoadMaps()
		{
			int Id = GetIdFromCookie();

			var result = _getBookMarkRoadMapService.Execute(Id);

			return Ok(result);
		}

		[HttpGet]
		public IActionResult GetCreatedRoadmaps()
		{
			var userId = GetIdFromCookie();

			var result = _getCreatedRoadMapService.Execute(userId);

			return Ok(result);
		}

		[HttpGet]
		public IActionResult GetCreatedLessons()
		{
			var userId = GetIdFromCookie();

			var result = _getCreatedLessonsService.Execute(userId);
			return Ok(result);
		}


		private int GetIdFromCookie()
		{
			var user = _currentContext.User;

			return user.Id;
			//var IdStr = HttpContext.User.Identities
			//	.FirstOrDefault()
			//	.Claims
			//	//.Where(c => c.Type == "NameIdentifier")
			//	.FirstOrDefault()
			//	.Value;

			//int Id = int.Parse(IdStr);
			//return Id;
		}
	}
}
