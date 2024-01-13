using ApiServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Interfaces
{
    public interface IOrganizationService
    {
        public Organization SelectOrganizationById(string id);
        public Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId);


    }
}
