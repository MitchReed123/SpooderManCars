using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.ManufacturerModels
{
    public class ManufacturerListItem
    {
        public int Id { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Locations { get; set; }
        [Display(Name = "Car Collection")]
        public virtual IEnumerable<CarItem> Cars { get; set; }
        public DateTime Founded { get; set; }
    }
}