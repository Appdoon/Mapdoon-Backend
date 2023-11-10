﻿using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.Homeworks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Homeworks.Query.GetHomeworkService
{
    public class HomeworkDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new();
        public int MinScore { get; set; }
        public List<HomeworkProgress> HomeworkProgresses { get; set; } = new();
    }
    public interface IGetHomeworkService : ITransientService
    {
        ResultDto<HomeworkDto> Execute(int id);
    }
    public class GetHomeworkService : IGetHomeworkService
    {
        private readonly IDatabaseContext _context;
        public GetHomeworkService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<HomeworkDto> Execute(int id)
        {
            throw new NotImplementedException();

            //try
            //{
            //    var homework = _context.Homeworks
            //        .Where(x => x.Id == id)
            //        .Include(h => h.Questions)
            //        .Include(h => h.HomeworkProgresses)
            //        .Select(h => new HomeworkDto()
            //        {
            //            Id = h.Id,
            //            Title = h.Title,
            //            Questions = h.Questions,
            //            MinScore = h.MinScore,
            //            HomeworkProgresses = h.HomeworkProgresses,

            //        }).FirstOrDefault();

            //    if (homework == null)
            //    {
            //        return new ResultDto<HomeworkDto>()
            //        {
            //            IsSuccess = false,
            //            Message = "تمرین یافت نشد!",
            //            Data = new HomeworkDto(),
            //        };
            //    }

            //    return new ResultDto<HomeworkDto>()
            //    {
            //        IsSuccess = true,
            //        Message = "تمرین ارسال شد",
            //        Data = homework,
            //    };
            //}
            //catch (Exception e)
            //{
            //    return new ResultDto<HomeworkDto>()
            //    {
            //        IsSuccess = false,
            //        Message = "ارسال ناموفق!",
            //        Data = new HomeworkDto(),
            //    };
            //}
        }
    }
}
