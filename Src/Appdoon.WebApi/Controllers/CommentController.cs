using Mapdoon.Application.Services.Comments.Command.CreateCommentService;
using Mapdoon.Application.Services.Comments.Command.UpdateCommentService;
using Mapdoon.Application.Services.Comments.Query.GetCommentsOfRoadmapService;
using Mapdoon.Application.Services.Comments.Command.DeleteCommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Mapdoon.Application.Services.JWTAuthentication.Command;
using Mapdoon.Common.Interfaces;

namespace Mapdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICreateCommentService _createCommentService;
        private readonly IUpdateCommentService _updateCommentService;
        private readonly IGetCommentsOfRoadmapService _getCommentsOfRoadmapService;
        private readonly IDeleteCommentService _deleteCommentService;
        public CommentController(ICreateCommentService createCommentService,
                                 IUpdateCommentService updateCommentService,
                                 IGetCommentsOfRoadmapService getCommentsOfRoadmapService,
                                 IDeleteCommentService deleteCommentService)
        {
            _createCommentService = createCommentService;
            _updateCommentService = updateCommentService;
            _getCommentsOfRoadmapService = getCommentsOfRoadmapService;
            _deleteCommentService = deleteCommentService;
        }
        [HttpPost("{RoadMapId}")]
        public IActionResult Post(int RoadMapId, CreateCommentDto comment, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _createCommentService.Execute(RoadMapId, userId, comment);
            return Ok(result);
        }
        [HttpPut("{RoadMapId}")]
        public IActionResult Put(int RoadMapId, UpdateCommentDto comment, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _updateCommentService.Execute(RoadMapId, userId, comment);
            return Ok(result);
        }
        [HttpGet("{RoadMapId}")]
        public IActionResult Get(int RoadMapId)
        {
            var result = _getCommentsOfRoadmapService.Execute(RoadMapId);
            return  Ok(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _deleteCommentService.Execute(id);
            return Ok(result);
        }
    }
}
