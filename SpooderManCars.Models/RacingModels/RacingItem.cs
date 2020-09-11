using SpooderManCars.Data;
using SpooderManCars.Models.ManufacturerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.RacingModels
{
    public class RacingItem
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerID { get; set; }
        public RacingViewManufacturerListItem Manufacturer { get; set; }
        public string TeamName { get; set; }
        public string BasedOutOF { get; set; }
        public string Drivers { get; set; }
        public RaceEvent RaceEvent { get; set; }
    }
}
