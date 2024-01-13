using ApiServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Interfaces
{
    public interface IStatisticService
    {
        public Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId, string connectionString);
        public Organization GetOrganizationWithMaxEmployees(string connectionString);

    }
}
