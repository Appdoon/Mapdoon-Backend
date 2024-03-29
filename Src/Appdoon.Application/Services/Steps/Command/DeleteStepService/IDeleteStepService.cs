﻿using Appdoon.Application.Interfaces;
using Appdoon.Common.Dtos;
using Mapdoon.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Steps.Command.DeleteStepService
{
	public interface IDeleteStepService : ITransientService
    {
		ResultDto Execute(int id);
	}

	public class DeleteStepService : IDeleteStepService
	{
		private readonly IDatabaseContext _context;

		public DeleteStepService(IDatabaseContext context)
		{
			_context = context;
		}
		public ResultDto Execute(int id)
		{
			try
			{

				var step = _context.Steps.Where(s => s.Id == id).FirstOrDefault();

				if(step == null)
                {
					return new ResultDto()
					{
						IsSuccess = false,
						Message = "این آیدی وجود ندارد!",
					};
				}

				step.IsRemoved = true;
				step.UpdateTime = DateTime.Now;
				_context.SaveChanges();

				return new ResultDto()
				{
					IsSuccess = true,
					Message = "قدم حدف شد.",
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
