﻿using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Common.Dtos;
using Appdoon.Common.Pagination;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService
{
    public class HomeworkProgressSubmissionDto
    {
        public int HomeworkId { get; set; }
        public int UserId { get; set; }
        public decimal Score { get; set; }
        public decimal MinScore { get; set; }
    }


    public interface ISubmitScoreService : ITransientService
    {
        ResultDto Execute(HomeworkProgressSubmissionDto hw);
    }
    public class SubmitScoreService : ISubmitScoreService
    {
        private readonly IDatabaseContext _context;
        public SubmitScoreService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(HomeworkProgressSubmissionDto submission)
        {
            try
            {
                var homeworkProgress = _context.HomeworkProgresses
                    .FirstOrDefault();
                homeworkProgress.Score = submission.Score;
                if (submission.Score >= submission.MinScore)
                {
                    homeworkProgress.IsDone = true;
                }
                else
                {
                    homeworkProgress.IsDone = false;
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نمره با موفقیت ثبت شد.",
                };
            }
            catch (Exception e)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا در ثبت نمره!",
                };
            }
        }

    }
}