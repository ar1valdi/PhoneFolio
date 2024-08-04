using BackendRestAPI.Model;
using BackendRestAPI.Requests.Dictionaries;
using BackendRestAPI.Services.Dictionaries;
using BackendRestAPI.Services.Dictionaries.DictionaryRequestMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendRestAPI.Controllers
{
    public class DictionaryController : ApiController
    {
        private readonly IDictionariesService _dictionariesService;
        private readonly IDictionaryRequestMapper _requestMapper;

        public DictionaryController(IDictionariesService dictionariesService, IDictionaryRequestMapper requestMapper)
        {
            _dictionariesService = dictionariesService;
            _requestMapper = requestMapper;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> FetchCategories()
        {
            // get categories from db
            var categoriesResult = await _dictionariesService.GetAllCategoriesAsync();

            var response = _requestMapper.MapCategoryListToResponse(categoriesResult.Value);
            return categoriesResult.Match(
                result => Ok(response),
                errors => Problem(errors)
            );
        }

        [HttpGet("subcategories/{categoryName}")]
        public async Task<IActionResult> FetchCategorySubategories(string categoryName)
        {
            // get subcategories with given categroy from db
            var subcategoriesResult = await _dictionariesService.GetCategorySubcategoriesAsync(categoryName);

            return subcategoriesResult.Match(
                result => Ok(_requestMapper.MapSubcategoryListToResponse(result)),
                errors => Problem(errors)
            );
        }
    }
}
