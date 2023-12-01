using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Comments.Command.DeleteCommentService
{
    public interface IDeleteCommentService : ITransientService
    {
        ResultDto Execute(int id);
    }
    public class DeleteCommnetService : IDeleteCommentService
    {
        private readonly IDatabaseContext _context;

        public DeleteCommnetService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int id)
        {
            try
            {
                var comment = _context.Comments
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (comment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این آیدی وجود ندارد!",
                    };
                }
                var replies = comment.replies;
                foreach(var reply in replies) 
                {
                    reply.IsRemoved = true;
                    reply.UpdateTime = DateTime.Now;
                }
                comment.IsRemoved = true;
                comment.UpdateTime = DateTime.Now;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = " نظر حدف شد.",
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
