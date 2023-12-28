using Appdoon.Application.Services.Progress.Command.DoneChildStep;
using Appdoon.Application.Services.Roadmaps.Command.CreateRoadmapService;
using Appdoon.Application.Services.Roadmaps.Command.UpdateRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.FilterRoadmapsService;
using Appdoon.Application.Services.Users.Query.IsUserBookMarkedRoadmapService;
using Mapdoon.Application.Services.Roadmaps;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private readonly IDoneChildStepService _doneChildStepService;
        private readonly ICurrentContext _currentContext;
        private readonly IRoadmapServiceFactory _roadmapServiceFactory;
        private readonly IIsUserBookMarkedRoadmapService _isUserBookMarkedRoadmapService;
        private readonly IRoadmapPermissionManager _roadmapPermissionManager;

        public RoadmapController(
                                  IDoneChildStepService doneChildStepService,
                                  IIsUserBookMarkedRoadmapService isUserBookMarkedRoadmapService,
                                  ICurrentContext currentContext,
                                  IRoadmapServiceFactory roadmapServiceFactory,
                                  IRoadmapPermissionManager roadmapPermissionManager)
        {
            _doneChildStepService = doneChildStepService;
            _currentContext = currentContext;
            _roadmapServiceFactory = roadmapServiceFactory;
            _isUserBookMarkedRoadmapService = isUserBookMarkedRoadmapService;
            _roadmapPermissionManager = roadmapPermissionManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int PageNumber = 1, int PageSize = 15)
        {
            var result = await _roadmapServiceFactory.GetAllRoadmapsService.Execute(PageNumber, PageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _roadmapServiceFactory.GetIndividualRoadmapService.Execute(id);
            return Ok(result);
        }

        [HttpGet("{RoadmapId}")]
        public async Task<IActionResult> UserRoadmap(int RoadmapId)
        {
            int UserId = _currentContext.User.Id;
            var result = await _roadmapServiceFactory.GetUserRoadmapService.Execute(RoadmapId, UserId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateRoadmapDto createRoadmapDto)
        {
            int userId = _currentContext.User.Id;
            var result = await _roadmapServiceFactory.CreateRoadmapService.Execute(createRoadmapDto, userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] UpdateRoadmapDto updateRoadmapDto, int id)
        {
            var result = await _roadmapServiceFactory.UpdateRoadmapService.Execute(updateRoadmapDto, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _roadmapServiceFactory.DeleteRoadmapService.Execute(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string SearchedText, int PageNumber, int PageSize)
        {
            var result = await _roadmapServiceFactory.SearchRoadmapsService.Execute(SearchedText, PageNumber, PageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(FilterDto fliterDto)
        {
            var result = await _roadmapServiceFactory.FilterRoadmapsService.Execute(fliterDto);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult HasUserRoadmap(int RoadmapId)
        {

            int UserId = _currentContext.User.Id;
            var result = _roadmapServiceFactory.CheckUserRegisterRoadmapService.Execute(RoadmapId, UserId);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult RegisterRoadmap(int RoadmapId)
        {
            int UserId = _currentContext.User.Id;
            var result = _roadmapServiceFactory.RegisterRoadmapService.Execute(RoadmapId, UserId);

            return Ok(result);
        }

        [HttpPost("{RoadmapId}")]
        public IActionResult BookmarkRoadmap(int RoadmapId)
        {
            int userId = _currentContext.User.Id;
            var result = _roadmapServiceFactory.BookmarkRoadmapService.Execute(RoadmapId, userId);

            return Ok(result);
        }

        [HttpGet("{RoadmapId}")]
        public IActionResult IsUserBookMarkedRoadmap(int RoadmapId)
        {
            int userId = _currentContext.User.Id;
            var result = _isUserBookMarkedRoadmapService.Execute(RoadmapId, userId);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPreviewRoadmap(int id)
        {
            var result = await _roadmapServiceFactory.GetPreviewRoadmapService.Execute(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DoneChildStep(int ChildStepId)
        {
            int UserId = _currentContext.User.Id;
            var result = _doneChildStepService.Execute(ChildStepId, UserId);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetPermissionView(int roadmapId)
        {
            int userId = _currentContext.User.Id;
            var result = _roadmapPermissionManager.GetPremissionViewDto(userId, roadmapId);

            return Ok(result);
        }
    }
}
