using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CsvReader.API.Controllers
{
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMemoryCache _memoryCache;
        public OrganizationController(IOrganizationService organizationService,IMemoryCache cache)
        {
            _organizationService = organizationService;
            _memoryCache = cache;

        }
        [HttpPost]
        [Route("organizationById")]
        public IActionResult GetOrganizationById(string id)
        {
            var result = _organizationService.SelectOrganizationById(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("organizationByCountryId")]
        public async Task<IActionResult> GetOrganizationByCountryId(string countryId)
        {
            string cacheKey = $"OrganizationData_{countryId}";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Organization> cachedResult))
            {
                return Ok(cachedResult);
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await _organizationService.SearchOrganizationByCountry(countryId);
            _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            string serializedResult = JsonConvert.SerializeObject(result);
            stopwatch.Stop();
            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} milliseconds for creating db");
            return Content(serializedResult, "application/json");
        }

    }
}
