using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models
{
    public class CarDetail
    {
        public int Id { get; set; }
        [Display(Name = "Manufacturer Id")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [Display(Name = "Garage Id")]
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }
        public string Transmission { get; set; }
        [Display(Name = "Value")]
        public decimal CarValue { get; set; }
    }
}
