using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.ManufacturerModels
{
    public class ManufacturerDetail
    {
        public int Id { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Locations { get; set; }
        public DateTime Founded { get; set; }
    }
}
