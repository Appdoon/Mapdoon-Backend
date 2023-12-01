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


namespace Mapdoon.Application.Services.Replies.Command.UpadteReplyService
{
    public class UpdateReplyDto
    {
        public string Text { get; set; }
    }

    public interface IUpdateReplyService : ITransientService
    {
        ResultDto Execute(int commentId, int userId, UpdateReplyDto reply);
    }
    public class UpdateReplyService : IUpdateReplyService
    {
        private readonly IDatabaseContext _context;
        public UpdateReplyService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int commentId, int userId, UpdateReplyDto reply)
        {
            try
            {
                var updatereply = _context.Replies
                 .FirstOrDefault(c => c.commentId == commentId && c.UserId == userId);
                if (updatereply == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "پاسخ نظر یافت نشد",
                    };
                }
                if (updatereply != null)
                {
                    updatereply.Text = reply.Text;
                    updatereply.IsEdited = true;
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "پاسخ نظر با موفقیت ویرایش شد.",
                };
            }
            catch (Exception e)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }
    }
}
