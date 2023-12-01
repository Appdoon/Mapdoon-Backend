using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Categories.Command.CreateCategoryService;
using Appdoon.Application.Services.Categories.Command.DeleteCategoryService;
using Appdoon.Application.Services.Categories.Command.UpdateCategoryService;
using Appdoon.Application.Services.Categories.Query.GetAllCategoriesService;
using Appdoon.Application.Services.Categories.Query.GetIndividualCategoryService;
using Appdoon.Application.Services.Categories.Query.SearchCategoriesService;
using Mapdoon.Common.Interfaces;

namespace Mapdoon.Application.Services.Categories
{
    public interface ICategoryServices : ITransientService
    {
        ICreateCategoryService CreateCategoryService { get; }
        IDeleteCategoryService DeleteCategoryService { get; }
        IUpdateCategoryService UpdateCategoryService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        IGetIndividualCategoryService GetIndividualCategoryService { get; }
        ISearchCategoriesService SearchCategoriesService { get; }
    }
    internal class CategoryServices : ICategoryServices
    {
        private readonly IDatabaseContext _context;
        public CategoryServices(IDatabaseContext context)
        {
            _context = context;
        }

        private ICreateCategoryService _createCategoryService;
        public ICreateCategoryService CreateCategoryService
        {
            get
            {
                return _createCategoryService ??= new CreateCategoryService(_context);
            }
        }

        private IDeleteCategoryService _deleteCategoryService;
        public IDeleteCategoryService DeleteCategoryService
        {
            get
            {
                return _deleteCategoryService ??= new DeleteCategoryService(_context);
            }
        }

        private IUpdateCategoryService _updateCategoryService;
        public IUpdateCategoryService UpdateCategoryService
        {
            get
            {
                return _updateCategoryService ??= new UpdateCategoryService(_context);
            }
        }

        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService ??= new GetAllCategoriesService(_context);
            }
        }

        private IGetIndividualCategoryService _getIndividualCategoryService;
        public IGetIndividualCategoryService GetIndividualCategoryService
        {
            get
            {
                return _getIndividualCategoryService ??= new GetIndividualCategoryService(_context);
            }
        }

        private ISearchCategoriesService _searchCategoriesService;
        public ISearchCategoriesService SearchCategoriesService
        {
            get
            {
                return _searchCategoriesService ??= new SearchCategoriesService(_context);
            }
        }
    }
}
