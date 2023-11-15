using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;
using Mapdoon.Application.Services.GradeHomeworks.Command.UpdateScoreService;
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
        public HomeworkProgressController(ISubmitScoreService submitscoreservice,
                                          IUpdateScoreService updateScoreService)
        {
            this._submitscoreservice = submitscoreservice;
            this._updatescoreservice = updateScoreService;
        }

        [HttpPost("submit-scores")]
        public JsonResult Post([FromBody] HomeworkProgressSubmissionDto submission)
        {
            var result = _submitscoreservice.Execute(submission);
            return new JsonResult(result);
        }
        [HttpPut("update-scores")]
        public JsonResult Put([FromBody] HomeworkProgressUpdateDto updateDto)
        {
            var result = _updatescoreservice.Execute(updateDto);
            return new JsonResult(result);
        }
    }
}