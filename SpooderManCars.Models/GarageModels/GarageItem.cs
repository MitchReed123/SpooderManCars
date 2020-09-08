using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models
{
    public class GarageItem
    {
        // Add Name of Collector?
        public int Id { get; set; }
        public string Location { get; set; }
        public IEnumerable<CarItem> CarCollection { get; set; }
        public double CollectionValue { get; set; }
    }
}
