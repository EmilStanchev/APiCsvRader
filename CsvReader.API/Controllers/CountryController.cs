using ApiDatabaseServices.ViewModels;
using ApiServices.Implementation;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsvReader.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [Authorize]
        [HttpPost]
        [Route("deleteCountry")]
        public IActionResult GetCoutnryById(int id,string accountId)
        {
            var result = _countryService.DeleteCountry(id,accountId);
            return StatusCode(result);
        }
    }
}
