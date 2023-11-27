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
		public JsonResult Get(int PageNumber = 1, int PageSize = 15)
		{
			var result = _getAllHomeworksService.Execute(PageNumber, PageSize);
			return new JsonResult(result);
		}

		[HttpGet("{id}")]
		public JsonResult Get(int id)
		{
			var result = _getHomeworkService.Execute(id);

			return new JsonResult(result);
		}
		[HttpGet("{creatorId}")]
		public JsonResult GetByCreatorId(int creatorId)
		{
			var result = _getHomeworksByCreator.Execute(creatorId);

			return new JsonResult(result);
		}

		[HttpGet]
		[Authorize(policy: "User")]
		public async Task<JsonResult> GetSubmission([FromServices] IGetHomeworkSubmissionsService getHomeworkSubmissionsService, [FromBody] GetUserSubmissionDto getUserSubmissionDto)
		{
			var result = await getHomeworkSubmissionsService.GetUserSubmission(getUserSubmissionDto, _currentContext.User.Id);

			return new JsonResult(result);
		}

		[HttpGet]
		[Authorize(policy: "Teacher")]
		public async Task<JsonResult> GetSubmissionForSpecificUser([FromServices] IGetHomeworkSubmissionsService getHomeworkSubmissionsService, int homeworkId, int UserId)
		{
			var result = await getHomeworkSubmissionsService.GetUserSubmission(new GetUserSubmissionDto() { HoemworkId = homeworkId }, UserId);

			return new JsonResult(result);
		}

		[HttpPost]
		[Authorize(policy: "User")]
		public async Task<JsonResult> SubmitHomework([FromServices] ISubmitHomeworkService submitHomeworkService, SubmitHomeworkDto submitHomeworkDto)
		{
			var result = await submitHomeworkService.SubmitHomework(submitHomeworkDto, _currentContext.User.Id);

			return new JsonResult(result);
		}

		[HttpPut]
		[Authorize(policy: "User")]
		public async Task<JsonResult> EditSubmission([FromServices] IEditHomeworkSubmissionService editHomeworkSubmissionService, EditHomeworkSubmissionDto editHomeworkSubmissionDto)
		{
			var result = await editHomeworkSubmissionService.EditSubmission(editHomeworkSubmissionDto, _currentContext.User.Id);

			return new JsonResult(result);
		}

		[HttpPost]
		public JsonResult Post(CreateHomeworkDto homeworkDto)
		{
			int userId = GetIdFromCookie();
			var result = _createHomeworkService.Execute(homeworkDto, userId);
			return new JsonResult(result);
		}

		[HttpPut("{id}")]
		public JsonResult Put(int id, UpdateHomeworkDto updateHomeworkDto)
		{
			var result = _updateHomeworkService.Execute(id, updateHomeworkDto);
			return new JsonResult(result);
		}

		[HttpDelete("{id}")]
		public JsonResult Delete(int id)
		{
			var result = _deleteHomeworkService.Execute(id);
			return new JsonResult(result);
		}

		private int GetIdFromCookie()
		{
			var user = _currentContext.User;
			return user.Id;
		}
	}
}
