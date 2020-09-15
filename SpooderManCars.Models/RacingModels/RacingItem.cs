using SpooderManCars.Data;
using SpooderManCars.Models.ManufacturerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.RacingModels
{
    public class RacingItem
    {
        public int Id { get; set; }
        [Display(Name = "Manufacturer Id")]
        public int ManufacturerID { get; set; }
        public virtual ManufacturerDetail Manufacturer { get; set; }
        [Display(Name = "Racing Team Name")]
        public string TeamName { get; set; }
        [Display(Name = "Based our of")]
        public string BasedOutOF { get; set; }
        [Display(Name = "Team Drivers")]
        public string Drivers { get; set; }
        [Display(Name = "Race Event")]
        public RaceEvent RaceEvent { get; set; }
    }
}
