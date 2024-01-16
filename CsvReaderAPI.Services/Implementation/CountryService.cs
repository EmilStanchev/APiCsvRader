using ApiDatabaseServices.ViewModels;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
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
        public int DeleteCountry(int countryId,string accountId)
        {
            try
            {
                var account = _databaseService.GetEntityById<Account>(accountId, "Accounts","Id");
                if (account.UserType=="Admin")
                {

                    _databaseService.SoftDeleteCountry(countryId);
                    return StatusCodes.Status200OK;
                }
                return StatusCodes.Status401Unauthorized;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCodes.Status400BadRequest;
            }
        }
        public Country GetCountryById(string countryId) 
        {
            return _databaseService.GetEntityById<Country>(countryId,"Countries","Id");
        }
        public List<Country> GetAll()
        {
            return _databaseService.SelectData<Country>("Countries");
        }
        public int CreateCoutry(CountryViewModel model)
        {
            try
            {
                var countries = GetAll();
                if (countries.Any(c=>c.CountryName==model.CountryName))
                {
                    return StatusCodes.Status400BadRequest;
                }
                var dbCountry = new Country() { CountryName=model.CountryName,Id=countries.Last().Id+1};
                _databaseService.InsertDataWithTableName(dbCountry,"Countries");
                return StatusCodes.Status201Created;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCodes.Status400BadRequest;
            }
        }
    }
}
