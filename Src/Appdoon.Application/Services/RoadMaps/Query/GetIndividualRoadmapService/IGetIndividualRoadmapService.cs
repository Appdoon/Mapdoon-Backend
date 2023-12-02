using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Roadmaps.Query.GetIndividualRoadmapService
{
    public interface IGetIndividualRoadmapService : ITransientService
    {
        ResultDto<IndividualRoadMapDto> Execute(int id);
    }
    public class GetIndividualRoadmapService : IGetIndividualRoadmapService
    {
        private readonly IDatabaseContext _context;
        public GetIndividualRoadmapService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<IndividualRoadMapDto> Execute(int id)
        {
            try
            {
                var roadmap = _context.RoadMaps
                    .Where(x => x.Id == id)
                    .Include(r => r.Categories)
                    .Include(r => r.Creatore)
                    .Include(r => r.Steps)
                    .ThenInclude(s => s.ChildSteps)
                    .ThenInclude(l => l.Linkers)
                    .Select(r => new IndividualRoadMapDto()
                    {
                        Id = r.Id,
                        CreateDate = r.InsertTime,
                        Description = r.Description,
                        ImageSrc = r.ImageSrc,
                        Stars = r.Stars,
                        Title = r.Title,
                        Categories = r.Categories,
                        RateCount = r.RateCount,
                        Steps = r.Steps.Select(s => new Step()
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Description = s.Description,
                            //IsDone = s.IsDone,
                            Link = s.Link,
                            IsRemoved = s.IsRemoved,
                            InsertTime = s.InsertTime,
                            UpdateTime = s.UpdateTime,
                            RoadMapId = s.RoadMapId,
                            ChildSteps = s.ChildSteps,
                        }).ToList(),
                        CreatorId = r.CreatoreId,
                        CreatorUserName = r.Creatore.Username,
                    }).FirstOrDefault();

                // get number of homeworks with this roadmap id
                roadmap.HomeworksNumber = _context.ChildSteps
                                                  .Where(cs => cs.HomeworkId == id && cs.HomeworkId != null)
                                                  .Count();

                if (roadmap == null)
                {
                    return new ResultDto<IndividualRoadMapDto>()
                    {
                        IsSuccess = false,
                        Message = "رود مپ یافت نشد!",
                        Data = new IndividualRoadMapDto(),
                    };
                }

                return new ResultDto<IndividualRoadMapDto>()
                {
                    IsSuccess = true,
                    Message = "رودمپ ها ارسال شد",
                    Data = roadmap,
                };
            }
            catch (Exception e)
            {
                return new ResultDto<IndividualRoadMapDto>()
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Data = new IndividualRoadMapDto(),
                };
            }
        }
    }
    public class IndividualRoadMapDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public string ImageSrc { get; set; } = string.Empty;
        public float? Stars { get; set; }
        public List<Category> Categories { get; set; }
        public List<Step> Steps { get; set; }
        public int CreatorId { get; set; }
        public string CreatorUserName { get; set; }
        public int HomeworksNumber { get; set; }
        public int RateCount;
    }
}
