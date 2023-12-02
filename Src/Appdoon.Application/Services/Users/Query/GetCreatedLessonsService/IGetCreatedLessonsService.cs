using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Mapdoon.Common.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Users.Query.GetCreatedLessonsService
{
    public class GetLessonsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        public string TopBannerSrc { get; set; } = string.Empty;
        public bool HasNewSrc { get; set; } = false;
    }
    public interface IGetCreatedLessonsService : ITransientService
    {
        Task<ResultDto<List<GetLessonsDto>>> Execute(int userId);
    }


    public class GetCreatedLessonsService : IGetCreatedLessonsService
    {
        private readonly IDatabaseContext _context;
        private readonly IFacadeFileHandler _facadeFileHandler;

        public GetCreatedLessonsService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
            _context = context;
            _facadeFileHandler = facadeFileHandler;
        }
        public async Task<ResultDto<List<GetLessonsDto>>> Execute(int userId)
        {
            try
            {
                var user = _context.Users
                        .AsNoTracking()
                        .Include(u => u.Roles)
                        .Include(u => u.CreatedLessons)
                        .Where(u => u.Id == userId)
                        .FirstOrDefault();

                if (user == null)
                {
                    return new ResultDto<List<GetLessonsDto>>()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد!",
                        Data = new(),
                    };
                }

                bool isTeacherOrAdmin = user.Roles
                    .Any(r => r.Name == UserRole.Teacher.ToString() || r.Name == UserRole.Admin.ToString());

                if (isTeacherOrAdmin == false)
                {
                    return new ResultDto<List<GetLessonsDto>>()
                    {
                        IsSuccess = false,
                        Message = "کابری دسترسی به ساخت مقاله ندارد",
                        Data = new(),
                    };
                }

                var lessons = user.CreatedLessons
                    .Select(cl => new GetLessonsDto()
                    {
                        Id = cl.Id,
                        Title = cl.Title,
                        CreatorName = user.Username,
                        TopBannerSrc = cl.TopBannerSrc,
                    }).ToList();

                foreach (var lesson in lessons)
                {
                    string url = await _facadeFileHandler.GetFileUrl("lessons", lesson.TopBannerSrc);
                    lesson.HasNewSrc = (url != lesson.TopBannerSrc);
                    lesson.TopBannerSrc = url;
                }

                return new ResultDto<List<GetLessonsDto>>()
                {
                    IsSuccess = true,
                    Data = lessons,
                    Message = "مقاله های ساخته شده با موفقیت ارسال شدند!",
                };
            }
            catch (Exception e)
            {
                return new ResultDto<List<GetLessonsDto>>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new(),
                };
            }
        }
    }
}
