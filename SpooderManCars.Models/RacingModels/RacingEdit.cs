using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.RacingModels
{
    public class RacingEdit
    {
        [Required]
        public int Id { get; set; }
        public int ManufacturerID { get; set; }
        public string TeamName { get; set; }
        public string BasedOutOF { get; set; }
        public List<int> Victories { get; set; } = new List<int>();
        public string Drivers { get; set; }
        public RaceEvent RaceEvent
        {
            get; set;
        }
    }
}
