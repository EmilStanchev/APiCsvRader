using ApiDatabaseServices.ViewModels;
using ApiServices.Implementation;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
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
        /*[HttpPost]

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
        [HttpPost]
        [Route("createAccount")]
        public IActionResult CreateAccount(Account model)
        {
            _databaseService.InsertData(model);
            return Ok();
        }
        [HttpPost]
        [Route("updateAccount")]
        public IActionResult UpdateAcc(Account model,string id)
        {
            _databaseService.UpdateData<Account>(model,id);
            return Ok();
        }
      */
        [HttpPost]
        [Route("deleteCountry")]
        public IActionResult GetCoutnryById(int id)
        {
            var result = _countryService.DeleteCountry(id);
            return StatusCode(result);
        }
    }
}
