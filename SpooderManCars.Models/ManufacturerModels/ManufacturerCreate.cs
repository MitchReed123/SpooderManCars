using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.ManufacturerModels
{
    public class ManufacturerCreate
    {
        public string CompanyName { get; set; }
        public string Locations { get; set; }
        public DateTime Founded { get; set; }
    }
}
