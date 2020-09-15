using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.ManufacturerModels
{
    public class ManufacturerCreate
    {
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        public string Locations { get; set; }
        [Required]
        public DateTime Founded { get; set; }
    }
}
