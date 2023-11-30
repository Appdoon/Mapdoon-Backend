using Appdoon.Application.Services.Homeworks.Command.CreateHomeworkService;
using Appdoon.Application.Services.Homeworks.Command.DeleteHomeworkService;
using Appdoon.Application.Services.Homeworks.Command.UpdateHomeworkService;
using Appdoon.Application.Services.Homeworks.Query.GetAllHomeworksService;
using Appdoon.Application.Services.Homeworks.Query.GetHomeworkService;
using Mapdoon.Application.Services.Homeworks.Command.EditHomeworkSubmission;
using Mapdoon.Application.Services.Homeworks.Command.SubmitHomeworkService;
using Mapdoon.Application.Services.Homeworks.Query.GetHomeworksByCreatorService;
using Mapdoon.Application.Services.Homeworks.Query.GetHomeworkSubmissions;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Appdoon.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class HomeworkController : ControllerBase
	{
		//Get
		private readonly IGetHomeworkService _getHomeworkService;
		//Create
		private readonly ICreateHomeworkService _createHomeworkService;
		//Update
		private readonly IUpdateHomeworkService _updateHomeworkService;
		//Delete
		private readonly IDeleteHomeworkService _deleteHomeworkService;
		//Get All
		private readonly IGetAllHomeworksService _getAllHomeworksService;
		// Get by creatorId
		private readonly IGetHomeworksByCreator _getHomeworksByCreator;

		private readonly ICurrentContext _currentContext;

		private readonly IWebHostEnvironment _env;

		public HomeworkController(IGetHomeworkService getHomeworkService,
								  ICreateHomeworkService createHomeworkService,
								  IUpdateHomeworkService updateHomeworkService,
								  IDeleteHomeworkService deleteHomeworkService,
								  IGetAllHomeworksService getAllHomeworksService,
								  IGetHomeworksByCreator getHomeworksByCreator,
								  ICurrentContext currentContext,
								  IWebHostEnvironment env)
		{
			_getHomeworkService = getHomeworkService;
			_createHomeworkService = createHomeworkService;
			_updateHomeworkService = updateHomeworkService;
			_deleteHomeworkService = deleteHomeworkService;
			_getAllHomeworksService = getAllHomeworksService;
			_getHomeworksByCreator = getHomeworksByCreator;
			_currentContext = currentContext;
			_env = env;
		}

		[HttpGet]
		public IActionResult Get(int PageNumber = 1, int PageSize = 15)
		{
			var result = _getAllHomeworksService.Execute(PageNumber, PageSize);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var result = _getHomeworkService.Execute(id);

			return Ok(result);
		}
		[HttpGet("{creatorId}")]
		public IActionResult GetByCreatorId(int creatorId)
		{
			var result = _getHomeworksByCreator.Execute(creatorId);

			return Ok(result);
		}

		[HttpGet]
		[Authorize(policy: "User")]
		public async Task<IActionResult> GetSubmission([FromServices] IGetHomeworkSubmissionsService getHomeworkSubmissionsService, [FromQuery] GetUserSubmissionDto getUserSubmissionDto)
		{
			var result = await getHomeworkSubmissionsService.GetUserSubmission(getUserSubmissionDto, _currentContext.User.Id);

			return Ok(result);
		}

		[HttpGet]
		[Authorize(policy: "Teacher")]
		public async Task<IActionResult> GetSubmissionForSpecificUser([FromServices] IGetHomeworkSubmissionsService getHomeworkSubmissionsService, int homeworkId, int UserId)
		{
			var result = await getHomeworkSubmissionsService.GetUserSubmission(new GetUserSubmissionDto() { HoemworkId = homeworkId }, UserId);

			return Ok(result);
		}

		[HttpPost]
		[Authorize(policy: "User")]
		public async Task<IActionResult> SubmitHomework([FromServices] ISubmitHomeworkService submitHomeworkService, SubmitHomeworkDto submitHomeworkDto)
		{
			var result = await submitHomeworkService.SubmitHomework(submitHomeworkDto, _currentContext.User.Id);

			return Ok(result);
		}

		[HttpPut]
		[Authorize(policy: "User")]
		public async Task<IActionResult> EditSubmission([FromServices] IEditHomeworkSubmissionService editHomeworkSubmissionService, EditHomeworkSubmissionDto editHomeworkSubmissionDto)
		{
			var result = await editHomeworkSubmissionService.EditSubmission(editHomeworkSubmissionDto, _currentContext.User.Id);

			return Ok(result);
		}

		[HttpPost]
		public IActionResult Post(CreateHomeworkDto homeworkDto)
		{
			int userId = GetIdFromCookie();
			var result = _createHomeworkService.Execute(homeworkDto, userId);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, UpdateHomeworkDto updateHomeworkDto)
		{
			var result = _updateHomeworkService.Execute(id, updateHomeworkDto);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var result = _deleteHomeworkService.Execute(id);
			return Ok(result);
		}

		private int GetIdFromCookie()
		{
			var user = _currentContext.User;
			return user.Id;
		}
	}
}
