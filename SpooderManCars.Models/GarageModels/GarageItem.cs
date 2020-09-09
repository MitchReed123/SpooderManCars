using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
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
        public IEnumerable<CarItem> CarCollection { get; set; }
        //public decimal CollectionValue { get; set; }
        public decimal CollectionValue
        {
            get
            {
                return GetTotalValue(CarCollection);
            }
            //set
            //{
                
            //}
        }

        //-- For potential use later -- Make sure to refactor CollectionValue
        private decimal GetTotalValue(IEnumerable<CarItem> cars)
        {
            decimal total = 0;
            foreach (CarItem car in cars)
            {
                total += car.CarValue;
            }
            return total;

        }
    }
}
