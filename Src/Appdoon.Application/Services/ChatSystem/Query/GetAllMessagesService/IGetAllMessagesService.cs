using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Replies;
using Mapdoon.Application.Services.Comments.Query.GetCommentsOfRoadmapService;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.ChatSystem.Query.GetAllMessagesService
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public string Username { get; set; }
        public int? ReplyMessageId { get; set; }
        public List<ChatMessageDto> Replies { get; set; } = new List<ChatMessageDto>();
    }
    public class AllChatMessagesDto
    {
        public List<ChatMessageDto> Messages { get; set; }
        public int RowCount { get; set; }
    }
    public interface IGetAllMessagesService : ITransientService
    {
        ResultDto<AllChatMessagesDto> Execute(int roadmapId , int PageNumber, int PageSize);
    }
    public class GetAllMessagesService : IGetAllMessagesService
    {
        private readonly IDatabaseContext _context;
        public GetAllMessagesService(IDatabaseContext contex)
        {
            _context = contex;
        }
        public ResultDto<AllChatMessagesDto> Execute(int roadmapId,int PageNumber, int PageSize)
        {
            try
            {
                int rowCount = 0;
                var message = _context.ChatMessages
                    .Include(m => m.Sender)
                    .Where(m => m.RoadMapId == roadmapId)
                    .Select(m => new ChatMessageDto
                    {
                        Id = m.Id,
                        Message = m.Message,
                        SenderId = m.SenderId,
                        Username = m.Sender.Username,
                        ReplyMessageId = m.ReplyMessageId,
                        Replies = new List<ChatMessageDto>()
                    })
                    .ToPaged(PageNumber, PageSize, out rowCount)
                    .ToList();
                foreach(var m in message)
                {
                    if(m.ReplyMessageId != null)
                    {
                        foreach(var r in message)
                        {
                            if (r.Id == m.ReplyMessageId)
                            {
                                var reply = new ChatMessageDto
                                {
                                    Id = m.Id,
                                    Message = m.Message,
                                    SenderId = m.SenderId,
                                    Username = _context.Users.FirstOrDefault(u => u.Id == m.SenderId).Username,
                                    ReplyMessageId = m.ReplyMessageId
                                };
                                r.Replies.Add(reply);
                            }
                        }
                    }
                    //var replis = _context.ChatMessages
                    //            .Include(r => r.Sender)
                    //            .Where(r => r.ReplyMessageId == m.Id)
                    //            .Select(r => new ChatMessageDto
                    //            {
                    //                Id = m.Id,
                    //                Message = m.Message,
                    //                SenderId = m.SenderId,
                    //                ReplyMessageId = m.ReplyMessageId,
                    //            }).FirstOrDefault();
                }
                if (message.Count == 0)
                {
                    return new ResultDto<AllChatMessagesDto>()
                    {
                        IsSuccess = false,
                        Message = "نظرات یافت نشد",
                        Data = new AllChatMessagesDto()
                    };
                }
                AllChatMessagesDto allchatmessages = new AllChatMessagesDto();
                allchatmessages.Messages = message;

                return new ResultDto<AllChatMessagesDto>()
                {
                    IsSuccess = true,
                    Message = "پیام  با پاسخ ارسال شدند.",
                    Data = allchatmessages
                };
            }
            catch (Exception e)
            {
                return new ResultDto<AllChatMessagesDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new AllChatMessagesDto()
                };
            }
        }
    }
}
