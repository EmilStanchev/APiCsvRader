using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.ViewModels
{
    public class OrganizationViewModel
    {
        [Required]
        public int Index { get; set; }
        [Required]

        public string Organization_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Website { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Founded { get; set; }
        [Required]
        public string Industry { get; set; }
        [Required]
        public int NumberOfEmployees { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
