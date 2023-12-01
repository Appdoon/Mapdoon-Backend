using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Replies.Command.DeleteReplyService
{
    public interface IDeleteReplyService : ITransientService
    {
        ResultDto Execute(int id);
    }
    public class DeleteReplyService : IDeleteReplyService
    {
        private readonly IDatabaseContext _context;

        public DeleteReplyService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int id)
        {
            try
            {
                var reply = _context.Replies
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (reply == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این آیدی وجود ندارد!",
                    };
                }

                reply.IsRemoved = true;
                reply.UpdateTime = DateTime.Now;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "پاسخ نظر حدف شد.",
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

