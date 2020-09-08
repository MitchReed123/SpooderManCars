using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models
{
    public class CreateGarage
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public double CollectionValue { get; set; }
    }
}
