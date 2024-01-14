using ApiServices.Interfaces;
using CsvReaderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Implementation
{
    public class CountryService: ICountryService
    {
        private readonly IDatabaseService _databaseService;
        public CountryService(IDatabaseService database)
        {
            _databaseService = database;
        }
        public int DeleteCountry(int countryId)
        {
            try
            {
                _databaseService.SoftDeleteCountry(countryId);
                return StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCodes.Status400BadRequest;
            }
        }
    }
}
