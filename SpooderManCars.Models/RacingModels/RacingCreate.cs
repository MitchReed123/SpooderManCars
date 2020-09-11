﻿using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models.RacingModels
{
    public class RacingCreate
    {
        //[ForeignKey(nameof(Manufacturer))]
        public int ManufacturerID { get; set; }
        //public virtual Manufacturer Manufacturer { get;  }
        public string TeamName { get; set; }
        public string BasedOutOF { get; set; }
        public string Drivers { get; set; }
        public RaceEvent RaceEvent { get; set; }
    }
    public class RacingEdit
    {
        public int Id { get; set; }
        public int ManufacturerID { get; set; }
        public string TeamName { get; set; }
        public string BasedOutOF { get; set; }
        public string Drivers { get; set; }
        public RaceEvent RaceEvent { get; set; }
    }
}
