using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mapdoon.Application.Services.ChatSystem.Query.GetAllMessagesService
{
	public class ChatMessageDto
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public int SenderId { get; set; }
		public string Username { get; set; }
		public DateTime CreatedAtDate { get; set; }
		public string CreatedAtTime { get; set; }
		public int? ReplyMessageId { get; set; }
		public string? ReplySenderUsername { get; set; }
		public string? RepliedMessage { get; set; }

		//public List<ChatMessageDto> Replies { get; set; } = new List<ChatMessageDto>();
	}
	public class AllChatMessagesDto
	{
		public List<ChatMessageDto> Messages { get; set; }
		public int RowCount { get; set; }
	}
	public interface IGetAllMessagesService : ITransientService
	{
		ResultDto<AllChatMessagesDto> Execute(int roadmapId, int PageNumber, int PageSize);
	}
	public class GetAllMessagesService : IGetAllMessagesService
	{
		private readonly IDatabaseContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICurrentContext _currentContext;
		private readonly IUserHubConnectionIdManager _userHubConnectionIdManager;

		public GetAllMessagesService(IDatabaseContext context, 
			                         IHttpContextAccessor httpContextAccessor, 
									 ICurrentContext currentContext,
									 IUserHubConnectionIdManager userHubConnectionIdManager)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_currentContext = currentContext;
			_userHubConnectionIdManager = userHubConnectionIdManager;
		}
		public ResultDto<AllChatMessagesDto> Execute(int roadmapId, int PageNumber, int PageSize)
		{
			try
			{
				//var userId = _currentContext.User?.Id;
				//var connectionId = _httpContextAccessor.HttpContext.Request?.Headers["connectionId"].ToString();
				//if(userId != null && userId != 0 && string.IsNullOrEmpty(connectionId) == false)
				//{
				//	_userHubConnectionIdManager.Add(userId.ToString(), connectionId);
				//}

				int rowCount = 0;
				var message = _context.ChatMessages
									  .Where(m => m.RoadMapId == roadmapId)
									  .Include(m => m.ReplyMessage)
									  .Include(m => m.Sender)
									  .Select(m => new ChatMessageDto()
									  {
										  Id = m.Id,
										  Message = m.Message,
										  SenderId = m.SenderId,
										  Username = m.Sender.Username,
										  ReplyMessageId = m.ReplyMessageId,
										  CreatedAtDate = m.InsertTime,
										  CreatedAtTime = m.InsertTime.ToString("hh:mm tt"),
										  RepliedMessage = m.ReplyMessageId == null ? null : m.ReplyMessage.Message,
										  ReplySenderUsername = m.ReplyMessageId == null ? null : m.Sender.Username,
									  })
									 .OrderBy(m => m.Id)
									 .ToPaged(PageNumber, PageSize, out rowCount)
									 .ToList();

				if(message.Count == 0)
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
				allchatmessages.RowCount = rowCount;

				return new ResultDto<AllChatMessagesDto>()
				{
					IsSuccess = true,
					Message = "پیام  با پاسخ ارسال شدند.",
					Data = allchatmessages
				};
			}
			catch(Exception e)
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
