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

    }
}
