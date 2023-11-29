using Appdoon.Application.Services.Categories.Command.CreateCategoryService;
using Appdoon.Application.Services.Categories.Command.UpdateCategoryService;
using Mapdoon.Application.Services.Categories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public ICategoryServices CategoryServices => _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public JsonResult Get(int PageNumber, int PageSize)
        {
            var result = CategoryServices.GetAllCategoriesService.Execute(PageNumber, PageSize);
            return new JsonResult(result);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var result = CategoryServices.GetIndividualCategoryService.Execute(id);
            return new JsonResult(result);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public JsonResult Post(CreateCategoryDto Category)
        {
            var result = CategoryServices.CreateCategoryService.Execute(Category);
            return new JsonResult(result);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] UpdateCategoryDto Category)
        {
            var result = CategoryServices.UpdateCategoryService.Execute(id, Category);
            return new JsonResult(result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = CategoryServices.DeleteCategoryService.Execute(id);
            return new JsonResult(result);
        }

        [HttpGet]
        public JsonResult Search(string SearchedText, int PageNumber, int PageSize)
        {
            var result = CategoryServices.SearchCategoriesService.Execute(SearchedText, PageNumber, PageSize);
            return new JsonResult(result);
        }
    }
}
