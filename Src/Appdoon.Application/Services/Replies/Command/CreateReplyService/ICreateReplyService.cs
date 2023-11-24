using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Replies;
using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;
using Microsoft.EntityFrameworkCore;

namespace Mapdoon.Application.Services.Replies.Command.CreateReplyService
{
    public class CreateReplyDto
    {
        public string Text { get; set; }
    }
    public interface ICreateReplyService : ITransientService
    {
        ResultDto Execute(int commentId, int userId, CreateReplyDto reply);
    }
    public class CreateReplyService : ICreateReplyService
    {
        private readonly IDatabaseContext _contetx;
        public CreateReplyService(IDatabaseContext context)
        {
            _contetx = context;
        }
        public ResultDto Execute(int commentId, int userId, CreateReplyDto reply)
        {
            try
            {
                Reply createreply = new Reply()
                {
                    Text = reply.Text,
                    UserId = userId,
                    commentId = commentId
                };
                _contetx.Replies.Add(createreply);
                var comment = _contetx.Comments
                    .Where(c => c.Id == commentId).First();
                comment.replies.Add(createreply);
                _contetx.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "پاسخ نظر با موفقیت ثبت شد.",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا در ثبت پاسخ نظر!",
                };
            }
        }
    }
}
