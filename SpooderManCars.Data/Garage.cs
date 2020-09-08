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
        public string Name { get; set; }
        public Guid CollectorId { get; set; }
        public string Location { get; set; }
        public ICollection<Car> CarCollection { get; set; }
        //public decimal CollectionValue { get; set; }
        public decimal CollectionValue
        {
            get
            {
                return GetTotalValue(CarCollection);
            }
            set
            {
                CollectionValue = GetTotalValue(CarCollection);
            }
        }

        //-- For potential use later -- Make sure to refactor CollectionValue
        private decimal GetTotalValue(ICollection<Car> cars)
        {
            decimal total = 0;
            foreach (Car car in cars)
            {
                total += car.CarValue;
            }
            return total;

        }
    }
}
