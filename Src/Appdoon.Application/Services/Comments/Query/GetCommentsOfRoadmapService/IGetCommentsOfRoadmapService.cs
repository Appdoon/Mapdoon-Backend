using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Replies;
using Mapdoon.Application.Services.GradeHomeworks.Query.GetAllHomeworkAnswerService;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mapdoon.Application.Services.Comments.Query.GetCommentsOfRoadmapService
{
    public class ReplyDto
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }
        public bool IsEdited { get; set; }
    }
    public class CommentDto
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }
        public bool IsEdited { get; set; }
        public List<ReplyDto> Replies { get; set; } 
    }
    public class AllCommentsRepliesDto
    {
        public List<CommentDto> AllComments { get; set;}
    }
    public interface IGetCommentsOfRoadmapService : ITransientService
    {
        ResultDto<AllCommentsRepliesDto> Execute(int roadmapId);
    }
    public class GetCommentsOfRoadmapService : IGetCommentsOfRoadmapService
    {
        private readonly IDatabaseContext _context;
        public GetCommentsOfRoadmapService(IDatabaseContext contex)
        {
            _context = contex;
        }
        public ResultDto<AllCommentsRepliesDto> Execute(int roadmapId)
        {
            try
            {
                var comment = _context.Comments
                    .Include(c => c.User)
                    .Where(c => c.RoadmapId == roadmapId)
                    .Select(c => new CommentDto
                    {
                        Username = c.User.Username,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt.Date.ToString("dd/MM/yyyy"),
                        IsEdited = c.IsEdited,
                        Replies = c.replies
                        .Select(cr => new ReplyDto
                        {
                            Username = cr.User.Username,
                            Text = cr.Text,
                            CreatedAt = c.CreatedAt.Date.ToString("dd/MM/yyyy"),
                            IsEdited = cr.IsEdited
                        })
                        .ToList()
                    })
                    .ToList();
                AllCommentsRepliesDto allCommentsReplies = new AllCommentsRepliesDto();
                allCommentsReplies.AllComments = comment;

                return new ResultDto<AllCommentsRepliesDto>()
                {
                    IsSuccess = true,
                    Message = "نظرات با پاسخ ارسال شدند.",
                    Data = allCommentsReplies
                };
            }
            catch (Exception e)
            {
                return new ResultDto<AllCommentsRepliesDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new AllCommentsRepliesDto()
                };
            }
        }
    }
}
