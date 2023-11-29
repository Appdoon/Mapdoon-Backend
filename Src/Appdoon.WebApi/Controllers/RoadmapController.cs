using Appdoon.Application.Services.Progress.Command.DoneChildStep;
using Appdoon.Application.Services.Roadmaps.Command.CreateRoadmapService;
using Appdoon.Application.Services.Roadmaps.Command.DeleteRoadmapService;
using Appdoon.Application.Services.Roadmaps.Command.UpdateRoadmapService;
using Appdoon.Application.Services.Roadmaps.Query.GetAllRoadmapsService;
using Appdoon.Application.Services.Roadmaps.Query.GetIndividualRoadmapService;
using Appdoon.Application.Services.RoadMaps.Command.BookmarkRoadmapService;
using Appdoon.Application.Services.RoadMaps.Command.RegisterRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.CheckUserRegisterRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.FilterRoadmapsService;
using Appdoon.Application.Services.RoadMaps.Query.GetPreviewRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.GetUserRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.SearchRoadmapsService;
using Appdoon.Application.Services.Users.Query.IsUserBookMarkedRoadmapService;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appdoon.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RoadmapController : ControllerBase
	{

		//Get All
		private readonly IGetAllRoadmapsService _getAllRoadmapsService;
		//Get Individual
		private readonly IGetIndividualRoadmapService _getIndividualRoadmapService;
		//Create
		private readonly ICreateRoadmapService _createRoadmapService;
		//Delete
		private readonly IDeleteRoadmapService _deleteRoadmapService;
		//Update
		private readonly IUpdateRoadmapService _updateRoadmapService;
		//Search
		private readonly ISearchRoadmapsService _searchRoadmapsService;
		//filter
		private readonly IFilterRoadmapsService _filterRoadmapsService;



		private readonly IGetUserRoadmapService _getUserRoadmapService;
		private readonly IRegisterRoadmapService _registerRoadmapService;
		private readonly IBookmarkRoadmapService _bookmarkRoadmapService;
		private readonly ICheckUserRegisterRoadmapService _checkUserRegisterRoadmapService;
		private readonly IGetPreviewRoadmapService _getPreviewRoadmapService;

		private readonly IDoneChildStepService _doneChildStepService;

		public readonly IIsUserBookMarkedRoadmapService _isUserBookMarkedRoadmapService;
		private readonly ICurrentContext _currentContext;
		private readonly IWebHostEnvironment _env;


		public RoadmapController(IGetAllRoadmapsService getAllRoadmapsService,
								  IGetIndividualRoadmapService getIndividualRoadmapService,
								  ICreateRoadmapService createRoadmapService,
								  IDeleteRoadmapService deleteRoadmapService,
								  IUpdateRoadmapService updateRoadmapService,
								  ISearchRoadmapsService searchRoadmapsService,
								  IFilterRoadmapsService filterRoadmapsService,
								  IGetUserRoadmapService getUserRoadmapService,
								  IRegisterRoadmapService registerRoadmapService,
								  IBookmarkRoadmapService bookmarkRoadmapService,
								  ICheckUserRegisterRoadmapService checkUserRegisterRoadmapService,
								  IGetPreviewRoadmapService getPreviewRoadmapService,
								  IDoneChildStepService doneChildStepService,
								  IIsUserBookMarkedRoadmapService isUserBookMarkedRoadmapService,
								  ICurrentContext currentContext,
								  IWebHostEnvironment env)
		{
			_getAllRoadmapsService = getAllRoadmapsService;
			_getIndividualRoadmapService = getIndividualRoadmapService;
			_createRoadmapService = createRoadmapService;
			_deleteRoadmapService = deleteRoadmapService;
			_updateRoadmapService = updateRoadmapService;
			_searchRoadmapsService = searchRoadmapsService;
			_filterRoadmapsService = filterRoadmapsService;
			_getUserRoadmapService = getUserRoadmapService;
			_registerRoadmapService = registerRoadmapService;
			_bookmarkRoadmapService = bookmarkRoadmapService;
			_checkUserRegisterRoadmapService = checkUserRegisterRoadmapService;
			_getPreviewRoadmapService = getPreviewRoadmapService;
			_doneChildStepService = doneChildStepService;
			_isUserBookMarkedRoadmapService = isUserBookMarkedRoadmapService;
			_currentContext = currentContext;
			_env = env;
		}

		// GET: api/<RoadmapController>
		[HttpGet]
		public JsonResult Get(int PageNumber = 1, int PageSize = 15)
		{
			var result = _getAllRoadmapsService.Execute(PageNumber, PageSize);
			return new JsonResult(result);
		}

		// GET api/<RoadmapController>/5
		[HttpGet("{id}")]
		public JsonResult Get(int id)
		{
			var result = _getIndividualRoadmapService.Execute(id);

			return new JsonResult(result);
		}

		[HttpGet("{RoadmapId}")]
		public JsonResult UserRoadmap(int RoadmapId)
		{
			int UserId = _currentContext.User.Id;
			var result = _getUserRoadmapService.Execute(RoadmapId, UserId);

			return new JsonResult(result);
		}

		// POST api/<RoadmapController>
		[HttpPost]
		public JsonResult Post()
		{
			int CreatorId = _currentContext.User.Id;
			var result = _createRoadmapService.Execute(Request, _env.ContentRootPath, CreatorId);
			return new JsonResult(result);
		}

		// PUT api/<RoadmapController>/5
		[HttpPut("{id}")]
		public JsonResult Put(int id)
		{
			var result = _updateRoadmapService.Execute(id, Request, _env.ContentRootPath);
			return new JsonResult(result);
		}

		// DELETE api/<RoadmapController>/5
		[HttpDelete("{id}")]
		public JsonResult Delete(int id)
		{
			var result = _deleteRoadmapService.Execute(id);
			return new JsonResult(result);
		}

		// GET api/<RoadmapController>
		[HttpGet]
		public JsonResult Search(string SearchedText, int PageNumber, int PageSize)
		{
			var result = _searchRoadmapsService.Execute(SearchedText, PageNumber, PageSize);
			return new JsonResult(result);
		}

		// GET api/<RoadmapController>
		[HttpPost]
		public JsonResult Filter(FilterDto fliterDto)
		{
			var result = _filterRoadmapsService.Execute(fliterDto);
			return new JsonResult(result);
		}

		[HttpGet]
		public JsonResult HasUserRoadmap(int RoadmapId)
		{

			int UserId = _currentContext.User.Id;
			var result = _checkUserRegisterRoadmapService.Execute(RoadmapId, UserId);

			return new JsonResult(result);
		}

		[HttpPost]
		public JsonResult RegisterRoadmap(int RoadmapId)
		{
			// should use cookies for geting userId not api call
			int UserId = _currentContext.User.Id;
			var result = _registerRoadmapService.Execute(RoadmapId, UserId);

			return new JsonResult(result);
		}

		[HttpPost("{RoadmapId}")]
		public JsonResult BookmarkRoadmap(int RoadmapId)
		{
			int userId = _currentContext.User.Id;
			var result = _bookmarkRoadmapService.Execute(RoadmapId, userId);

			return new JsonResult(result);
		}

		[HttpGet("{RoadmapId}")]
		public JsonResult IsUserBookMarkedRoadmap(int RoadmapId)
		{
			int userId = _currentContext.User.Id;
			var result = _isUserBookMarkedRoadmapService.Execute(RoadmapId, userId);
			return new JsonResult(result);
		}


		[HttpGet("{id}")]
		public JsonResult GetPreviewRoadmap(int id)

		{
			var result = _getPreviewRoadmapService.Execute(id);

			return new JsonResult(result);
		}

		[HttpPost]
		public JsonResult DoneChildStep(int ChildStepId)
		{
			int UserId = _currentContext.User.Id;
			var result = _doneChildStepService.Execute(ChildStepId, UserId);

			return new JsonResult(result);
		}
	}
}
