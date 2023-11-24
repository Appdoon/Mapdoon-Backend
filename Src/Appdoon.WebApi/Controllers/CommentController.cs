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
    public class CommentControllers : ControllerBase
    {
        private readonly ICreateCommentService _createCommentService;
        private readonly IUpdateCommentService _updateCommentService;
        private readonly IGetCommentsOfRoadmapService _getCommentsOfRoadmapService;
        private readonly IDeleteCommentService _deleteCommentService;
        public CommentControllers(ICreateCommentService createCommentService,
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
        public JsonResult Post(int RoadMapId, CreateCommentDto comment, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _createCommentService.Execute(RoadMapId, userId, comment);
            return new JsonResult(result);
        }
        [HttpPut("{RoadMapId}")]
        public JsonResult Put(int RoadMapId, UpdateCommentDto comment, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _updateCommentService.Execute(RoadMapId, userId, comment);
            return new JsonResult(result);
        }
        [HttpGet("{RoadMapId}")]
        public JsonResult Get(int RoadMapId)
        {
            var result = _getCommentsOfRoadmapService.Execute(RoadMapId);
            return new JsonResult(result);
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _deleteCommentService.Execute(id);
            return new JsonResult(result);
        }
    }
}
