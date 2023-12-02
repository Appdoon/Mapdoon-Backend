using Appdoon.Application.Services.Progress.Command.DoneChildStep;
using Appdoon.Application.Services.Roadmaps.Command.CreateRoadmapService;
using Appdoon.Application.Services.Roadmaps.Command.UpdateRoadmapService;
using Appdoon.Application.Services.RoadMaps.Query.FilterRoadmapsService;
using Appdoon.Application.Services.Users.Query.IsUserBookMarkedRoadmapService;
using Mapdoon.Application.Services.Roadmaps;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private readonly IDoneChildStepService _doneChildStepService;
        private readonly ICurrentContext _currentContext;
        private readonly IWebHostEnvironment _env;
        private readonly IRoadmapServiceFactory _roadmapServiceFactory;
        private readonly IIsUserBookMarkedRoadmapService _isUserBookMarkedRoadmapService;

        public RoadmapController(
                                  IDoneChildStepService doneChildStepService,
                                  IIsUserBookMarkedRoadmapService isUserBookMarkedRoadmapService,
                                  ICurrentContext currentContext,
                                  IWebHostEnvironment env,
                                  IRoadmapServiceFactory roadmapServiceFactory)
        {
            _doneChildStepService = doneChildStepService;
            _currentContext = currentContext;
            _env = env;
            _roadmapServiceFactory = roadmapServiceFactory;
            _isUserBookMarkedRoadmapService = isUserBookMarkedRoadmapService;
        }

        // GET: api/<RoadmapController>
        [HttpGet]
        public async Task<IActionResult> Get(int PageNumber = 1, int PageSize = 15)
        {
            var result = await _roadmapServiceFactory.GetAllRoadmapsService.Execute(PageNumber, PageSize);
            return Ok(result);
        }

        // GET api/<RoadmapController>/5
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

        // POST api/<RoadmapController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateRoadmapDto createRoadmapDto)
        {
            int userId = _currentContext.User.Id;
            var result = await _roadmapServiceFactory.CreateRoadmapService.Execute(createRoadmapDto, userId);
            return Ok(result);
        }

        // PUT api/<RoadmapController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] UpdateRoadmapDto updateRoadmapDto, int id)
        {
            var result = await _roadmapServiceFactory.UpdateRoadmapService.Execute(updateRoadmapDto, id);
            return Ok(result);
        }

        // DELETE api/<RoadmapController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _roadmapServiceFactory.DeleteRoadmapService.Execute(id);
            return Ok(result);
        }

        // GET api/<RoadmapController>
        [HttpGet]
        public async Task<IActionResult> Search(string SearchedText, int PageNumber, int PageSize)
        {
            var result = await _roadmapServiceFactory.SearchRoadmapsService.Execute(SearchedText, PageNumber, PageSize);
            return Ok(result);
        }

        // GET api/<RoadmapController>
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
            // should use cookies for geting userId not api call
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
    }
}
