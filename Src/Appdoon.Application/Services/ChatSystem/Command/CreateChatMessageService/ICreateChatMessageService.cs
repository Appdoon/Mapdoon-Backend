using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Services.Comments.Command.CreateCommentService;
using Mapdoon.Common.Interfaces;
using Mapdoon.Domain.Entities.Chat;

namespace Mapdoon.Application.Services.ChatSystem.Command.CreateChatMessageService
{
    public class CreateMessageDto
    {
        public string Message { get; set; }
        public string? ImageUrl { get; set; }
        public int? ReplyMessageId { get; set; }
    }
    public interface ICreateChatMessageService : ITransientService
    {
        ResultDto Execute(int roadmapId, int userId, CreateMessageDto message);
    }
    public class CreateChatMessageService : ICreateChatMessageService
    {
        private readonly IDatabaseContext _context;
        public CreateChatMessageService(IDatabaseContext context)
        {
            this._context = context;
        }
        public ResultDto Execute(int roadmapId, int userId, CreateMessageDto message)
        {
            try
            {
                ChatMessage chatmessage = new ChatMessage()
                {
                    Message = message.Message,
                    ImageUrl = message.ImageUrl,
                    ReplyMessageId = message.ReplyMessageId,
                    SenderId = userId,
                    RoadMapId = roadmapId
                };
                _context.ChatMessages.Add(chatmessage);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "پیام شما با موفقیت ارسال شد.",
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
