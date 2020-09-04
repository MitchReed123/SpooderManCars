using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models
{
    public class GarageItem
    {
        // Add Name of Collector?
        public int Id { get; set; }
        public string Location { get; set; }
        public ICollection<Cars> CarCollection { get; set; }
        public double CollectionValue { get; set; }
    }
}
