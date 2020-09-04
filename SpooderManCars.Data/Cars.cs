using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Data
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }


        [ForeignKey(nameof(Garage))]
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        [Required]
        public Guid OwnerID { get; set; }

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
