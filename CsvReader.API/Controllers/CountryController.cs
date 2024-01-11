using ApiServices.Implementation;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CsvReader.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly IDatabaseService _databaseService;
        public CountryController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        [HttpPost]

        [Route("countryById")]
        public IActionResult GetCoutnryById(int id)
        {
            var result = _databaseService.GetEntityById<Country>(id, "Countries", "Id");
            return Ok(result);
        }
    }
}
