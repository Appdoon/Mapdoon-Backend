using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Users.Query.GetRegisteredRoadMapService;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Application.Services.ChatSystem.Query.GetAllMessagesService;
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
		Task<ResultDto> Execute(int roadmapId, int userId, CreateMessageDto message);
    }

    public class CreateChatMessageService : ICreateChatMessageService
    {
        private readonly IDatabaseContext _context;
		private readonly IGetRegisteredRoadMapService _getRegisteredRoadMapService;
		private readonly IWebSocketMessageSender _webSocketMessageSender;

		public CreateChatMessageService(IDatabaseContext context, IGetRegisteredRoadMapService getRegisteredRoadMapService, IWebSocketMessageSender webSocketMessageSender)
        {
            this._context = context;
            _getRegisteredRoadMapService = getRegisteredRoadMapService;
            _webSocketMessageSender = webSocketMessageSender;

		}
        public async Task<ResultDto> Execute(int roadmapId, int userId, CreateMessageDto message)
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

                var webSocketMessage = new ChatMessageDto()
                {
                    Id = chatmessage.Id,
                    Message = chatmessage.Message,
                    SenderId = chatmessage.SenderId,
                    Username = chatmessage.Sender.Username,
                    ReplyMessageId = chatmessage.ReplyMessageId,
                };
                await SendToGroup(webSocketMessage, roadmapId);

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

        public async Task SendToGroup(ChatMessageDto chatMessage, int roadmapId)
        {
            var getUsersResult = await _getRegisteredRoadMapService.Execute(roadmapId);
            foreach(var user in getUsersResult.Data)
            {
                _webSocketMessageSender.SendToUser("ReceiveMessage", user.Id.ToString(), chatMessage);
            }
        }
    }
}
