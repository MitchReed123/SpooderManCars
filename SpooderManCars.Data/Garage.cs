using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Data
{
    public class Garage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid CollectorId { get; set; }
        [Required]
        public string Location { get; set; }
        public virtual ICollection<Car> CarCollection { get; set; } 
        public decimal CollectionValue
        {
            get
            {
                decimal testing = 0m;
                foreach (var item in CarCollection)
                {
                    testing += item.CarValue;
                }
                return testing;
            }
        }
    }
}

