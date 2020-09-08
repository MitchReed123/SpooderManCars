using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.ManufacturerModels
{
    public class ManufacturerListItem
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Locations { get; set; }
        public IEnumerable<CarItem> Cars { get; set; }
        public DateTime Founded { get; set; }
    }
}
