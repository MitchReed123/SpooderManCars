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
        public Guid CollectorId { get; set; }
        public string Location { get; set; }
        public ICollection<Cars> Cars { get; set; }
        public double CollectionValue { get; set; }
    }
}
