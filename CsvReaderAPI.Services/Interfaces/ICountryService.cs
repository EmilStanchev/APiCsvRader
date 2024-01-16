using ApiServices.ViewModels;
using CsvReaderAPI.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Interfaces
{
    public interface ICountryService
    {
        public int DeleteCountry(int countryId,string accountId);
        public Country GetCountryById(string countryId);
        public List<Country> GetAll();
        public int CreateCoutry(CountryViewModel model);
        public string GetMostUsedCountryName();
    }
}
