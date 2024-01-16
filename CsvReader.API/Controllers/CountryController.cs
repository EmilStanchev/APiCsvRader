using ApiDatabaseServices.ViewModels;
using ApiServices.Implementation;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using PdfSharp;

namespace CsvReader.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMemoryCache _memoryCache;
        public CountryController(ICountryService countryService,IMemoryCache cache)
        {
            _countryService = countryService;
            _memoryCache = cache;
        }
        [HttpGet]
        [Route("getAllCountries")]
        public IActionResult GetAllCountries()
        {
            string cacheKey = $"AllCountries";

            if (_memoryCache.TryGetValue(cacheKey, out List<Country> cachedResult))
            {
                return Ok(cachedResult);
            }
            var result = _countryService.GetAll();

            _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(10));

            return Ok(result);
        }
        [HttpPost]
        [Route("getCountryById")]

        public IActionResult GetCountryById(string countryId)
        {
            var result = _countryService.GetCountryById(countryId);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteCountry")]
        public IActionResult GetCoutnryById(int id,string accountId)
        {
            var result = _countryService.DeleteCountry(id,accountId);
            return StatusCode(result);
        }
        [Authorize]
        [HttpPost]
        [Route("createCountry")]

        public IActionResult CreateCountry(CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _countryService.CreateCoutry(model);
            return StatusCode(result);
        }

    }
}
