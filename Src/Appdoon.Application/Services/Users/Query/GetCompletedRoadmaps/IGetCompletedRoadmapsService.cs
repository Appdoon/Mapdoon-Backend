using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Users.Query.GetBookMarkRoadMapService;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Application.Interfaces;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Application.Services.Users.Query.GetCompletedRoadmaps
{
	public class CompeletedRoadMapDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string ImageSrc { get; set; }
		public bool HasNewSrc { get; set; } = false;
	}

	public interface IGetCompletedRoadmapsService : ITransientService
	{
		Task<ResultDto<List<CompeletedRoadMapDto>>> Execute(int userId);
	}

	public class GetCompletedRoadmapsService : IGetCompletedRoadmapsService
	{
		private readonly IDatabaseContext _context;
		private readonly IFacadeFileHandler _facadeFileHandler;

		public GetCompletedRoadmapsService(IDatabaseContext context, IFacadeFileHandler facadeFileHandler)
        {
			_context = context;
			_facadeFileHandler = facadeFileHandler;
		}
        public async Task<ResultDto<List<CompeletedRoadMapDto>>> Execute(int userId)
		{
			try
			{
				var user = _context.Users
					.Where(r => r.Id == userId)
					.Include(r => r.SignedRoadMaps)
						.ThenInclude(sr => sr.Steps)
							.ThenInclude(s => s.StepProgresses)
					.FirstOrDefault();

				if(user == null)
				{
					return new ResultDto<List<CompeletedRoadMapDto>>()
					{
						IsSuccess = false,
						Message = "کابر یافت نشد!",
						Data = new(),
					};
				}

				var compeletedRoadmaps = user.SignedRoadMaps
											 .Where(sr => sr.Steps
											                .All(s => s.StepProgresses
															           .Any(sp => sp.UserId == userId && ((sp.IsRequired == false) || sp.IsDone == true))));


				var result = compeletedRoadmaps
					.Select(r => new CompeletedRoadMapDto()
					{
						Title = r.Title,
						ImageSrc = r.ImageSrc,
						Id = r.Id,
					}).ToList();

				foreach(var roadmap in result)
				{
					string url = await _facadeFileHandler.GetFileUrl("roadmaps", roadmap.ImageSrc);
					roadmap.HasNewSrc = (url != roadmap.ImageSrc);
					roadmap.ImageSrc = url;
				}

				return new ResultDto<List<CompeletedRoadMapDto>>()
				{
					IsSuccess = true,
					Message = "رودمپ های اتمام یافته یوزر دریافت شد",
					Data = result,
				};
			}
			catch(Exception e)
			{
				return new ResultDto<List<CompeletedRoadMapDto>>()
				{
					IsSuccess = false,
					Message = e.Message,
					Data = new(),
				};
			}
		}
	}
}
