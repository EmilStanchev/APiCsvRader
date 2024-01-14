using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetOrganizationByCountryId(string countryId, int page = 1)
        {
            int pageSize = 100;
            string cacheKey = $"OrganizationData_{countryId}_Page{page}_Size{pageSize}";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Organization> cachedResult))
            {
                return Ok(cachedResult);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            var allResults = await _organizationService.SearchOrganizationByCountry(countryId);
            var paginatedResults = allResults.Skip((page - 1) * pageSize).Take(pageSize);

            _memoryCache.Set(cacheKey, paginatedResults, TimeSpan.FromMinutes(10));

            string serializedResult = JsonConvert.SerializeObject(paginatedResults);

            stopwatch.Stop();
            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} milliseconds for creating db");

            return Content(serializedResult, "application/json");
        }
        [HttpGet]
        [Route("organizationByNumberOfEmployees")]
        public IActionResult GetOrganizationByNumberOfEmployees()
        {
            var result = _organizationService.GetOrganizationWithMaxEmployees();
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("getPdfForOrganizaiton")]

        public IActionResult GeneratePdf(string id)
        {
            try
            {
                string fileName = $"{id}-Info.pdf";
                byte[] pdfBytes = _organizationService.ReturnPdfFileForORganization(id);
                var contentDisposition = new Microsoft.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
