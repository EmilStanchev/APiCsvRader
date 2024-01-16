using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.ViewModels
{
    public class CountryViewModel
    {
        [Required]
        public string CountryName { get; set; }
    }
}
