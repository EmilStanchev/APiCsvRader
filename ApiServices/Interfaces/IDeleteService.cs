using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Interfaces
{
    public interface IDeleteService
    {
        public void SoftDeleteCountry(int countryId, string connectionString);
        public void SoftDeleteOrganization(string organizationId, string connectionString);
        public void SoftDeleteAccount(string accountId, string connectionString);

    }
}
