using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.CarModels
{
    public class CarEdit
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Manufacturer Id")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Garage Id")]
        public int GarageId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [RegularExpression(@"\d\d\d\d{4, 4}", ErrorMessage = "Please enter a four digit year")]
        public int Year { get; set; }
        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }
        public string Transmission { get; set; }
        [Display(Name = "Value")]
        public decimal CarValue { get; set; }
    }
}
