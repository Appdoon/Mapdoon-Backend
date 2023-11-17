﻿using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.Homeworks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mapdoon.Application.Services.GradeHomeworks.Query.GetAllHomeworkAnswerService
{
    public class HomeworkAnswerDto
    {
        public string Answer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AllHomeworkAnswerDto
    {
        public List<HomeworkAnswerDto> Answers { get; set; }
    }
    public interface IGetAllHomeworkAnswerService : ITransientService
    {
        public ResultDto<AllHomeworkAnswerDto> Execute(int homeworkId);
    }
    public class GetAllHomeworkAnswerService : IGetAllHomeworkAnswerService
    {
        private readonly IDatabaseContext _context;
        public GetAllHomeworkAnswerService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<AllHomeworkAnswerDto> Execute(int homeworkId)
        {
            try
            {
                var homeworkprogress = _context.HomeworkProgresses
                .Include(hp => hp.User)
                .Where(hp => hp.HomeworkId == homeworkId)
                .Select(hp => new HomeworkAnswerDto
                {
                    Answer = hp.Answer,
                    FirstName = hp.User.FirstName,
                    LastName = hp.User.LastName
                })
                .ToList();
                AllHomeworkAnswerDto allHomeworkAnswer = new AllHomeworkAnswerDto();
                allHomeworkAnswer.Answers = homeworkprogress;
                return new ResultDto<AllHomeworkAnswerDto>()
                {
                    IsSuccess = true,
                    Message = "تکالیف با پاسخ ارسال شدند.",
                    Data = allHomeworkAnswer
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<AllHomeworkAnswerDto>()
                {
                    IsSuccess = false,
                    Message = "ارسال ناموفق ‌‌تکالیف!",
                    Data = new AllHomeworkAnswerDto()
                };
            }
        }
    }
}
