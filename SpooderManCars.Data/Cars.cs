using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Data
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }


        [ForeignKey(nameof(Garage))]
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public CarType CarType { get; set; }
        public string Transmission { get; set; }

        

    }

    public enum CarType
    {
        Compact,
        MiniVan,
        Luxury,
        Sport,
        SUV,
        Exotic
    }
}
