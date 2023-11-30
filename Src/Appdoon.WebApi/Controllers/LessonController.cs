using Appdoon.Application.Services.Lessons.Command.CreateLessonService;
using Appdoon.Application.Services.Lessons.Command.DeleteLessonService;
using Appdoon.Application.Services.Lessons.Command.UpdateLessonService;
using Appdoon.Application.Services.Lessons.Query.GetAllLessonsService;
using Appdoon.Application.Services.Lessons.Query.GetIndividualLessonService;
using Appdoon.Application.Services.Lessons.Query.SearchLessonsService;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        //Get All
        private readonly IGetAllLessonsService _getAllLessonsService;
        //Get Individual
        private readonly IGetIndividualLessonService _getLessonService;
        //Create
        private readonly ICreateLessonService _createLessonService;
        //Delete
        private readonly IDeleteLessonService _deleteLessonService;
        //Update
        private readonly IUpdateLessonService _updateLessonService;
        //search 
        private readonly ISearchLessonsService _searchLessonsService;
        private readonly ICurrentContext _currentContext;
        private readonly IWebHostEnvironment _env;

        public LessonController(IGetAllLessonsService getAllLessonsService,
                                IGetIndividualLessonService getLessonService,
                                ICreateLessonService createLessonService,
                                IDeleteLessonService deleteLessonService,
                                IUpdateLessonService updateLessonService,
                                ISearchLessonsService searchLessonsService,
                                ICurrentContext currentContext,
                                IWebHostEnvironment env)
        {
            _getAllLessonsService = getAllLessonsService;
            _getLessonService = getLessonService;
            _createLessonService = createLessonService;
            _deleteLessonService = deleteLessonService;
            _updateLessonService = updateLessonService;
            _searchLessonsService = searchLessonsService;
            _currentContext = currentContext;
            _env = env;
        }

        // GET: api/<LessonController>
        [HttpGet]
        public async Task<IActionResult> Get(int PageNumber, int PageSize)
        {
            var result = await _getAllLessonsService.Execute(PageNumber, PageSize);
            return Ok(result);
        }

        // GET api/<LessonController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _getLessonService.Execute(id);
            return Ok(result);
        }

        // POST api/<LessonController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateLessonDto createLessonDto)
        {
            var userId = GetIdFromCookie();

            var result = await _createLessonService.Execute(createLessonDto, userId);
            return Ok(result);
        }

        // PUT api/<LessonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] UpdateLessonDto updateLessonDto, int id)
        {
            var result = await _updateLessonService.Execute(updateLessonDto, id);
            return Ok(result);
        }

        // DELETE api/<LessonController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _deleteLessonService.Execute(id);
            return Ok(result);
        }

        // GET api/<LessonController>
        [HttpGet]
        public async Task<IActionResult> Search(string SearchedText, int PageNumber, int PageSize)
        {
            var result = await _searchLessonsService.Execute(SearchedText, PageNumber, PageSize);
            return Ok(result);
        }

        private int GetIdFromCookie()
        {
            var user = _currentContext.User;

            return user.Id;
        }
    }
}
