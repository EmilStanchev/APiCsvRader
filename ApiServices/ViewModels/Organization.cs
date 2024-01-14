using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.ViewModels
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
        public override string ToString()
        {
            return $"Organization Information:\n" +
                   $"Index: {Index}\n" +
                   $"Organization ID: {Organization_Id}\n" +
                   $"Name: {Name}\n" +
                   $"Website: {Website}\n" +
                   $"Description: {Description}\n" +
                   $"Founded: {Founded}\n" +
                   $"Industry: {Industry}\n" +
                   $"Number of Employees: {NumberOfEmployees}\n" +
                   $"Country ID: {CountryId}";
        }
    }
}
