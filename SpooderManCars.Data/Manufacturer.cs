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
        public string CompanyName { get; set; }
        public string Locations { get; set; }
        public ICollection<Car> Cars { get; set; }
        public DateTime Founded { get; set; }
    }
}
