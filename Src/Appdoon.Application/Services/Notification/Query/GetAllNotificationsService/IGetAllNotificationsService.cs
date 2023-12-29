using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Notification.Query.GetAllNotificationsService
{
	public interface IGetAllNotificationsService : ITransientService
	{
		Task<ResultDto<List<GetNotificationResultDto>>> GetAllNotifications(GetNotificationDto input, int userId);
	}

	public class GetNotificationDto
	{
		public bool UnSeenNotificationsOnly { get; set; } = true;
    }

	public class GetNotificationResultDto
	{
		public int NotificationId { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }

	public class GetAllNotificationsService : IGetAllNotificationsService
	{
		private readonly IDatabaseContext _databaseContext;

		public GetAllNotificationsService(IDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;

		}
		public async Task<ResultDto<List<GetNotificationResultDto>>> GetAllNotifications(GetNotificationDto input, int userId)
		{
			try
			{
				var result = await _databaseContext.Notifications
												   .Where(n => n.ReceiverId == userId)
												   .Where(n => input.UnSeenNotificationsOnly == false ? true : n.IsSeen == false)
												   .Select(n => new GetNotificationResultDto
												   {
													   NotificationId = n.Id,
													   Message = n.Message,
													   CreateDate = n.InsertTime,
												   }).ToListAsync();

				return new ResultDto<List<GetNotificationResultDto>>()
				{
					Data = result,
					IsSuccess = true,
					Message = "Success",
				};
			}
			catch(Exception ex)
			{
				return new ResultDto<List<GetNotificationResultDto>>
				{
					Data = new(),
					IsSuccess = false,
					Message = ex.Message
				};
			}
		}
	}

}
