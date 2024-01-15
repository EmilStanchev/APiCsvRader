using ApiServices.ViewModels;
using CsvReaderAPI.Services.ViewModels;
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
        public Organization GetOrganizationWithMaxEmployees();
        public byte[] ReturnPdfFileForORganization(string id);
        public int CreateOrganization(OrganizationViewModel model);
        public int DeleteOrganization(string organizationId, string accountId);

    }
}
