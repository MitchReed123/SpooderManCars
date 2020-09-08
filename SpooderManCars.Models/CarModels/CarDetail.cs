﻿using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models
{
    public class CarDetail
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public CarType CarType { get; set; }
        public string Transmission { get; set; }
    }
}
