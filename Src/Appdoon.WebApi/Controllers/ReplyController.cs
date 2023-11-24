using Mapdoon.Application.Services.Replies.Command.CreateReplyService;
using Mapdoon.Application.Services.Replies.Command.UpadteReplyService;
using Mapdoon.Application.Services.Replies.Command.DeleteReplyService;
using Microsoft.AspNetCore.Mvc;
using Mapdoon.Common.Interfaces;


namespace Mapdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReplyController
    {
        private readonly ICreateReplyService _createReplyService;
        private readonly IUpdateReplyService _updateReplyService;
        private readonly IDeleteReplyService _deleterReplyService;
        public ReplyController(ICreateReplyService createReplyService,
                                IUpdateReplyService updateReplyService,
                                IDeleteReplyService deleteReplyService)
        {
            _createReplyService = createReplyService;
            _updateReplyService = updateReplyService;
            _deleterReplyService = deleteReplyService;  
        }
        [HttpPost("{commentId}")]
        public JsonResult Post(int commentId, CreateReplyDto reply, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _createReplyService.Execute(commentId, userId, reply);
            return new JsonResult(result);
        }
        [HttpPut("{commentId}")]
        public JsonResult Put(int commentId, UpdateReplyDto reply, [FromServices] ICurrentContext currentContext)
        {
            int userId = currentContext.User.Id;
            var result = _updateReplyService.Execute(commentId, userId, reply);
            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _deleterReplyService.Execute(id);
            return new JsonResult(result);
        }
    }
}
