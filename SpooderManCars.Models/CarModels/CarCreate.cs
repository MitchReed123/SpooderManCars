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
        [Display(Name = "Manufacturer Id")]
        public int ManufacturerId { get; set; }
        [Required]
        [Display(Name = "Garage Id")]
        public int GarageId { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [RegularExpression(@"\d\d\d\d{4, 4}", ErrorMessage = "Please enter a four digit year")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        [Display(Name = "Value")]
        public decimal CarValue { get; set; }
    }
}
