using Appdoon.Application.Interfaces;
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
                    .Include(hp => hp.Homework)
                    .FirstOrDefault(hp => hp.HomeworkId == submission.HomeworkId && hp.UserId == submission.UserId);
                homeworkProgress.Score = submission.Score;
                var childstepprogress = _context.ChildStepProgresses
                .FirstOrDefault(hp => hp.ChildStep.HomeworkId == homeworkProgress.Homework.Id);
                if(childstepprogress == null)
                {
                    var childStepId = _context.ChildSteps
                        .FirstOrDefault(hp => hp.HomeworkId == submission.HomeworkId).Id;
                    childstepprogress = new ChildStepProgress()
                    {
                        ChildStepId = childStepId,
                        UserId = submission.UserId,
                        IsRequired = true
                    };
                    _context.ChildStepProgresses.Add(childstepprogress);
                }
                if (submission.Score >= homeworkProgress.Homework.MinScore)
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
                    Message = e.Message,
                };
            }
        }

    }
}
