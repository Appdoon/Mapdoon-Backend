using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Appdoon.Domain.Entities.RoadMaps;

namespace Mapdoon.Application.Services.Comments.Command.UpdateCommentService
{
    public class UpdateCommentDto
    {
        public string Text { get; set; }
    }
    public interface IUpdateCommentService : ITransientService
    {
        ResultDto Execute(int roadmapId, int userId, UpdateCommentDto comment);
    }
    public class UpdateCommentService : IUpdateCommentService
    {
        private readonly IDatabaseContext _context;
        public UpdateCommentService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int roadmapId, int userId, UpdateCommentDto comment)
        {
            try
            {
                var updatecomment = _context.Comments
                 .FirstOrDefault(c => c.RoadmapId == roadmapId && c.UserId == userId);
                if (updatecomment != null) 
                {
                    updatecomment.Text = comment.Text;
                    updatecomment.IsEdited = true;
                }
                if (updatecomment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نظر پیدا نشد",
                    };
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نظر با موفقیت ویرایش شد.",
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
