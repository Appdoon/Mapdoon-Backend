﻿using Appdoon.Application.Interfaces;
using Appdoon.Application.Validatores.CategoryValidatore;
using Appdoon.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Application.Services.Categories.Command.DeleteCategoryService
{
    public interface IDeleteCategoryService
    {
        ResultDto Execute(int id);
    }

    public class DeleteCategoryService : IDeleteCategoryService
    {
		private readonly IDatabaseContext _context;

		public DeleteCategoryService(IDatabaseContext context)
		{
			_context = context;
		}
		public ResultDto Execute(int id)
		{
			try
			{
				var cat = _context.Categories.Where(s => s.Id == id).FirstOrDefault();
				if(cat == null)
                {
					return new ResultDto()
					{
						IsSuccess = false,
						Message = "این آیدی وجود ندارد!",
					};
				}

				cat.IsRemoved = true;
				cat.RemoveTime = DateTime.Now;
				_context.SaveChanges();

				return new ResultDto()
				{
					IsSuccess = true,
					Message = "دسته حذف شد.",
				};
			}
			catch (Exception e)
			{
				return new ResultDto()
				{
					IsSuccess = false,
					Message = "خطا در حذف دسته!",
				};
			}
		}
	}
}
