using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;


namespace Data.Models
{
    public class CsvData
    {
        [Name("Index")]
        public int Index { get; set; }
        [Name("Organization Id")]
        public string Organization_Id { get; set; }
        [Name("Name")]
        public string Name { get; set; }
        [Name("Website")]
        public string Website { get; set; }
        [Name("Country")]
        public string Country { get; set; }
        [Name("Description")]
        public string Description { get; set; }
        [Name("Founded")]
        public int Founded { get; set; }
        [Name("Industry")]
        public string Industry { get; set; }
        [Name("Number of employees")]
        public int NumberOfEmployees { get; set; }
        public override string ToString()
        {
            return $"{Index}, {Organization_Id}, {Name}, {Website}, {Country}, {Description}, {Founded}, {Industry}, {NumberOfEmployees}";
        }
    }
}
