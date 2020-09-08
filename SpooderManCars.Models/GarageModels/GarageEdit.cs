using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.GarageModels
{
    public class GarageEdit
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
