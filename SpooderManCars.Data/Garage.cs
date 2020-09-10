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
        public IEnumerable<Car> CarCollection { get; set; }
        public decimal CollectionValue
        {
            get
            {
                return GetTotalValue(CarCollection);
            }
        }

        private decimal GetTotalValue(IEnumerable<Car> cars)
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

