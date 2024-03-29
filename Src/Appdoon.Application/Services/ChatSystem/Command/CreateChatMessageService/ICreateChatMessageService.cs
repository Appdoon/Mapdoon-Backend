﻿using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Application.Services.ChatSystem.Query.GetRegisterdUsersService;
using Mapdoon.Application.Services.Notifications.Command.SendNotificationService;
using Mapdoon.Common.Interfaces;
using Mapdoon.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.ChatSystem.Command.CreateChatMessageService
{
	public class CreateMessageDto
	{
		public string Message { get; set; }
		public string? ImageUrl { get; set; }
		public int? ReplyMessageId { get; set; }
	}

	public class WebSocketChatMessage
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
	}

	public interface ICreateChatMessageService : ITransientService
	{
		Task<ResultDto> Execute(int roadmapId, int userId, CreateMessageDto message);
	}

	public class CreateChatMessageService : ICreateChatMessageService
	{
		private readonly IDatabaseContext _context;
		private readonly IGetRegisterdUsersService _getRegisteredRoadMapService;
		private readonly ISendNotificationService _sendNotificationService;
		private readonly IWebSocketMessageSender _webSocketMessageSender;

		public CreateChatMessageService(IDatabaseContext context,
										IGetRegisterdUsersService getRegisteredRoadMapService,
										ISendNotificationService sendNotificationService,
										IWebSocketMessageSender webSocketMessageSender)
		{
			this._context = context;
			_getRegisteredRoadMapService = getRegisteredRoadMapService;
			_sendNotificationService = sendNotificationService;
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

				var sender = _context.Users
									 .Include(u => u.Roles)
									 .Where(u => u.Id == userId)
									 .FirstOrDefault();

				var webSocketMessage = new WebSocketChatMessage()
				{
					Id = chatmessage.Id,
					Message = chatmessage.Message,
					SenderId = chatmessage.SenderId,
					Username = sender.Username,
					CreatedAtDate = DateTime.Now,
					CreatedAtTime = DateTime.Now.ToString("hh:mm tt"),
					ReplyMessageId = chatmessage.ReplyMessageId,
					//RepliedMessage = ,
					//ReplySenderId = ,
				};

				if(webSocketMessage.ReplyMessageId != null)
				{
					var repliedMessage = _context.ChatMessages.Where(m => m.Id == webSocketMessage.ReplyMessageId).FirstOrDefault();
					webSocketMessage.RepliedMessage = repliedMessage.Message;

					var replyMessageSender = _context.Users.Where(u => u.Id == repliedMessage.SenderId).Select(u => u.Username).FirstOrDefault();
					webSocketMessage.ReplySenderUsername = replyMessageSender;
				}

				var getUsersResult = _getRegisteredRoadMapService.Execute(roadmapId);

				await SendToGroup(getUsersResult.Data, webSocketMessage);

				var isTeacherSender = _context.RoadMaps.Any(r => r.Id == roadmapId && r.CreatoreId == userId);
				if(isTeacherSender)
				{
					await SendNotification(getUsersResult.Data, roadmapId);
				}

				return new ResultDto()
				{
					IsSuccess = true,
					Message = "پیام شما با موفقیت ارسال شد.",
				};
			}
			catch(Exception e)
			{
				return new ResultDto()
				{
					IsSuccess = false,
					Message = e.Message,
				};
			}
		}

		private async Task SendToGroup(List<RegisterdUsersDto> getUsersResult, WebSocketChatMessage chatMessage)
		{
			foreach(var user in getUsersResult)
			{
				_webSocketMessageSender.SendToUser("ReceiveMessage", user.Id.ToString(), chatMessage);
			}
		}

		private async Task SendNotification(List<RegisterdUsersDto> getUsersResult, int roadmapId)
		{
			foreach(var user in getUsersResult)
			{
				var roadmapTitle = _context.RoadMaps
										   .Where(r => r.Id == roadmapId)
										   .Select(r => r.Title)
										   .FirstOrDefault();

				await _sendNotificationService.SendNotification($"از طرف استاد پیغام جدیدی در گروه رودمپ {roadmapTitle}  یک پیغام جدید ارسال شده است!", user.Id);
			}
		}
	}
}
