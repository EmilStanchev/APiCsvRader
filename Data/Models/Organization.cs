using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Organization
    {
        public int Index { get; set; }
        public string Organization_Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public string Industry { get; set; }
        public int NumberOfEmployees { get; set; }
        public int CountryId { get; set; }
    }
}
