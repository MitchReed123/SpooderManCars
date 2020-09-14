using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Data
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Locations { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        [Required]
        public DateTime Founded { get; set; }
    }
}
