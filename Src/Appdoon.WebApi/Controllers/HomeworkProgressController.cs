using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;
using Mapdoon.Application.Services.GradeHomeworks.Command.UpdateScoreService;
using Mapdoon.Application.Services.GradeHomeworks.Query.GetAllHomeworkAnswerService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeworkProgressController : ControllerBase
    {
        private readonly ISubmitScoreService _submitscoreservice;
        private readonly IUpdateScoreService _updatescoreservice;
        private readonly IGetAllHomeworkAnswerService _getAllHomeworkAnswerService;
        public HomeworkProgressController(ISubmitScoreService submitscoreservice,
                                          IUpdateScoreService updateScoreService,
                                          IGetAllHomeworkAnswerService getAllHomeworkAnswerService)
        {
            this._submitscoreservice = submitscoreservice;
            this._updatescoreservice = updateScoreService;
            this._getAllHomeworkAnswerService = getAllHomeworkAnswerService;
        }

        [HttpGet("{homeworkId}")]
        public IActionResult Get(int homeworkId)
        {
            var result = _getAllHomeworkAnswerService.Execute(homeworkId);
            return Ok(result);
        }

        [HttpPost("submit-scores")]
        public IActionResult Post([FromBody] HomeworkProgressSubmissionDto submission)
        {
            var result = _submitscoreservice.Execute(submission);
            return Ok(result);
        }
        [HttpPut("update-scores")]
        public IActionResult Put([FromBody] HomeworkProgressUpdateDto updateDto)
        {
            var result = _updatescoreservice.Execute(updateDto);
            return Ok(result);
        }
    }
}