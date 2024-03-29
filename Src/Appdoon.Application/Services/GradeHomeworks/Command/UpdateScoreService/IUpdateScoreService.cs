﻿using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Mapdoon.Application.Services.Notifications.Command.SendNotificationService;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.GradeHomeworks.Command.UpdateScoreService
{
    public class HomeworkProgressUpdateDto
    {
        public int HomeworkId { get; set; }
        public int UserId { get; set; }
        public decimal Score { get; set; }
    }

    public interface IUpdateScoreService : ITransientService
    {
        ResultDto Execute(HomeworkProgressUpdateDto updateDto);
    }
    public class UpdateScoreService : IUpdateScoreService
    {
        private readonly IDatabaseContext _context;
		private readonly ISendNotificationService _sendNotificationService;

		public UpdateScoreService(IDatabaseContext context, ISendNotificationService sendNotificationService)
        {
            _context = context;
            _sendNotificationService = sendNotificationService;

		}
        public ResultDto Execute(HomeworkProgressUpdateDto updateDto)
        {
            try
            {
                var homeworkProgress = _context.HomeworkProgresses
                    .Include(hp => hp.Homework)
                    .FirstOrDefault(hp => hp.HomeworkId == updateDto.HomeworkId && hp.UserId == updateDto.UserId);
                homeworkProgress.Score = updateDto.Score;
                var childstepprogress = _context.ChildStepProgresses
                .FirstOrDefault(hp => hp.ChildStep.HomeworkId == homeworkProgress.Homework.Id);
                if (updateDto.Score >= homeworkProgress.Homework.MinScore)
                {
                    homeworkProgress.IsDone = true;
                    childstepprogress.IsDone = true;
                }
                else
                {
                    homeworkProgress.IsDone = false;
                    childstepprogress.IsDone = false;
                }
                _context.SaveChanges();

                var teacher = _context.Users.FirstOrDefault(u => u.Id == homeworkProgress.Homework.CreatorId);

                _sendNotificationService.SendNotification($"نمره شما در رودمپ {homeworkProgress.Homework.Title} توسط {teacher.Username} به مقدار {updateDto.Score} ثبت شد!", updateDto.UserId);

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نمره با موفقیت ویرایش شد.",
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
