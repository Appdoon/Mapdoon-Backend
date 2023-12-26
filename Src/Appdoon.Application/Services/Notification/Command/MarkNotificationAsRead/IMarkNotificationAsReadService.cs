using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Notification.Command.MarkNotificationAsRead
{
    public interface IMarkNotificationAsReadService : ITransientService
    {
        Task<ResultDto<bool>> MarkNotificationAsRead(int notificationId);
        Task<ResultDto<bool>> MarkAllNotificationsAsRead();
    }

    public class MarkNotificationAsReadService : IMarkNotificationAsReadService
    {
        private readonly IDatabaseContext _databaseContext;

        public MarkNotificationAsReadService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ResultDto<bool>> MarkNotificationAsRead(int notificationId)
        {
            try
            {
                var notification = await _databaseContext.Notifications.FindAsync(notificationId);
                if (notification == null)
                {
                    return new ResultDto<bool>()
                    {
                        IsSuccess = false,
                        Message = "Notification not found",
                    };
                }

                notification.IsSeen = true;

                await _databaseContext.SaveChangesAsync();
                return new ResultDto<bool>()
                {
                    IsSuccess = true,
                    Message = "Success",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<bool>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultDto<bool>> MarkAllNotificationsAsRead()
        {
            try
            {
                var notifications = await _databaseContext.Notifications
                                                          .Where(n => n.IsSeen == false)
                                                          .ToListAsync();
                if (notifications == null)
                {
                    return new ResultDto<bool>()
                    {
                        IsSuccess = false,
                        Message = "Notifications not found",
                    };
                }

                foreach (var notification in notifications)
                {
                    notification.IsSeen = true;
                }

                await _databaseContext.SaveChangesAsync();
                return new ResultDto<bool>()
                {
                    IsSuccess = true,
                    Message = "Success",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<bool>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
