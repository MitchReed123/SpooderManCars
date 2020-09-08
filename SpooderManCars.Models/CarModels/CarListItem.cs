using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.CarModels
{
    public class CarItem
    {
        
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public Guid OwnerID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public CarType CarType { get; set; }
        public string Transmission { get; set; }
        public decimal CarValue { get; set; }
    }
}
