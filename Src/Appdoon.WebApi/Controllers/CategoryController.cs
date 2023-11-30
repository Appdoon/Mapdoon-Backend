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
        public IActionResult Get(int PageNumber, int PageSize)
        {
            var result = CategoryServices.GetAllCategoriesService.Execute(PageNumber, PageSize);
            return Ok(result);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = CategoryServices.GetIndividualCategoryService.Execute(id);
            return Ok(result);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post(CreateCategoryDto Category)
        {
            var result = CategoryServices.CreateCategoryService.Execute(Category);
            return Ok(result);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCategoryDto Category)
        {
            var result = CategoryServices.UpdateCategoryService.Execute(id, Category);
            return Ok(result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = CategoryServices.DeleteCategoryService.Execute(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Search(string SearchedText, int PageNumber, int PageSize)
        {
            var result = CategoryServices.SearchCategoriesService.Execute(SearchedText, PageNumber, PageSize);
            return Ok(result);
        }
    }
}
