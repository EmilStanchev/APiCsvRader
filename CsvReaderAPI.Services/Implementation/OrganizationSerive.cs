using ApiServices.Interfaces;
using ApiServices.ViewModels;
using CsvReaderAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Implementation
{
    public class OrganizationSerive : IOrganizationService
    {
        private readonly IDatabaseService _dbService;
        public OrganizationSerive(IDatabaseService dbService)
        {
            _dbService = dbService;
        }
        public Organization SelectOrganizationById(string id)
        {
            return _dbService.GetEntityById<Organization>(id, "Organizations", "Organization_Id");
        }
        public async Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId)
        {
            return await _dbService.SearchOrganizationByCountry(countryId);
        }
        public Organization GetOrganizationWithMaxEmployees()
        {
            return _dbService.GetOrganizationWithMaxEmployees();
        }

    }
}
