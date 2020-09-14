using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Models
{
    public class GarageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
        [Display(Name ="Car Collection")]
        public IEnumerable<CarItem> CarCollection { get; set; }
        [Display(Name = "Collection Value")]
        public decimal CollectionValue
        {
            get
            {
                //if (CarCollection == null)
                //{return null;}

                decimal testing = 0m;
                foreach (var item in CarCollection)
                {
                   testing += item.CarValue;
                }
                return testing;
            }
        }
    }
    public class GarageSimpleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
