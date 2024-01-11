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
        public IActionResult GetCoutnryById(string id)
        {
            var result = _databaseService.GetEntityById<Country>(id, "Countries", "Id");
            return Ok(result);
        }
        [HttpPost]
        [Route("organizationById")]
        public IActionResult GetOrganizationById(string id)
        {
            var result = _databaseService.GetEntityById<Organization>(id, "Organizations", "Organization_Id");
            return Ok(result);
        }
    }
}
