using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.RoadMaps.Query.GetUserRoadmapService
{
	public interface IGetUserRoadmapService : ITransientService
    {
		Task<ResultDto<IndividualRoadMapDto>> Execute(int RoadmapId, int UserId);
	}
	public class GetUserRoadmapService : IGetUserRoadmapService
	{
		private readonly IDatabaseContext _context;
		private readonly IFacadeFileHandler _facadeFileHandler;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserHubConnectionIdManager _userHubConnectionIdManager;

		public GetUserRoadmapService(IDatabaseContext context, 
			                         IFacadeFileHandler facadeFileHandler,
									 IHttpContextAccessor httpContextAccessor,
									 IUserHubConnectionIdManager userHubConnectionIdManager)
		{
			_context = context;
			_facadeFileHandler = facadeFileHandler;
			_httpContextAccessor = httpContextAccessor;
			_userHubConnectionIdManager = userHubConnectionIdManager;
		}
		public async Task<ResultDto<IndividualRoadMapDto>> Execute(int RoadmapId, int UserId)
		{
			try
			{
				var connectionId = _httpContextAccessor.HttpContext.Request?.Headers["connectionId"].ToString();
				if(UserId != 0 && string.IsNullOrEmpty(connectionId) == false)
				{
					_userHubConnectionIdManager.Add(UserId.ToString(), connectionId);
				}

				bool hasRoadmap = _context.Users
					.Include(u => u.SignedRoadMaps)
					.Where(u => u.Id == UserId)
					.FirstOrDefault()
					.SignedRoadMaps
					.Any(srp => srp.Id == RoadmapId);

				var roadmap = _context.RoadMaps
					.Where(x => x.Id == RoadmapId)
					.Include(r => r.Categories)
					.Include(r => r.Creatore)
					.Select(r => new IndividualRoadMapDto()
					{
						Id = r.Id,
						CreateDate = r.InsertTime,
						CreatorUserName = r.Creatore.Username,
						Description = r.Description,
						ImageSrc = r.ImageSrc,
						Stars = r.Stars,
						Title = r.Title,
						Categories = r.Categories,
						Steps = r.Steps.Select(s => new Step()
						{
							Id = s.Id,
							Title = s.Title,
							Description = s.Description,
							Link = s.Link,
							IsRemoved = s.IsRemoved,
							InsertTime = s.InsertTime,
							UpdateTime = s.UpdateTime,
							RoadMapId = s.RoadMapId,
							ChildSteps = s.ChildSteps.Select(c => new ChildStep()
							{
								Id = c.Id,
								Title = c.Title,
								Description = c.Description,
								InsertTime = c.InsertTime,
								UpdateTime = c.UpdateTime,
								IsRemoved = c.IsRemoved,
								HomeworkId = c.HomeworkId,
								Link = c.Link,
								Linkers = c.Linkers,
								ChildStepProgresses = c.ChildStepProgresses.Where(csp => csp.UserId == UserId).ToList(),
							}).ToList(),
							StepProgresses = s.StepProgresses.Where(sp => sp.UserId == UserId).ToList(),
						}).ToList(),
					}).FirstOrDefault();

				if(roadmap == null)
				{
					return new ResultDto<IndividualRoadMapDto>()
					{
						IsSuccess = false,
						Message = "رود مپ یافت نشد!",
						Data = new IndividualRoadMapDto(),
					};
				}

				roadmap.HomeworksNumber = _context.ChildSteps
												  .Where(cs => cs.HomeworkId == RoadmapId && cs.HomeworkId != null)
                .Count();

                string url = await _facadeFileHandler.GetFileUrl("roadmaps", roadmap.ImageSrc);
                roadmap.HasNewSrc = (url != roadmap.ImageSrc);
                roadmap.ImageSrc = url;

                return new ResultDto<IndividualRoadMapDto>()
				{
					IsSuccess = true,
					Message = "رودمپ ها ارسال شد",
					Data = roadmap,
				};
			}
			catch(Exception e)
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
		public string CreatorUserName { get; set; }
		public DateTime CreateDate { get; set; }
		public int HomeworksNumber { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; }
		public string ImageSrc { get; set; } = string.Empty;
		public float? Stars { get; set; }
		public List<Category> Categories { get; set; }
		public bool HasNewSrc { get; set; } = false;
		public List<Step> Steps { get; set; }
	}
}
