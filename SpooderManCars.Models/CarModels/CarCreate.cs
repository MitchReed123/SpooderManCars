using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.CarModels
{
    public class CarCreate
    {
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int GarageId { get; set; }

        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public CarType CarType { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public decimal CarValue { get; set; }
    }
}
