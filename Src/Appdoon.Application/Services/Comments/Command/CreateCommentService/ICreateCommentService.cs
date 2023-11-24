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

namespace Mapdoon.Application.Services.Comments.Command.CreateCommentService
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
    }
    public interface ICreateCommentService : ITransientService
    {
        ResultDto Execute(int roadmapId, int userId, CreateCommentDto comment);
    }
    public class CreateCommentService : ICreateCommentService
    {
        private readonly IDatabaseContext _context;
        public CreateCommentService(IDatabaseContext context)
        {
            this._context = context;
        }
        public ResultDto Execute(int roadmapId, int userId, CreateCommentDto comment)
        {
            try
            {
                Comment createcomment = new Comment()
                {
                    UserId = userId,
                    RoadmapId = roadmapId,
                    Text = comment.Text
                };
                _context.Comments.Add(createcomment);
                var RoadMap = _context.RoadMaps
                .Where(r => r.Id == roadmapId).First();
                RoadMap.Comments.Add(createcomment);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نظر با موفقیت ثبت شد.",
                };
            }
            catch(Exception ex) 
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا در ثبت نظر!",
                };
            }
        }
    }
}
