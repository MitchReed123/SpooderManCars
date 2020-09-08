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
        public double CollectionValue { get; set; }


        // -- For potential use later -- Make sure to refactor CollectionValue
        //private double GetTotalValue(ICollection<Car> cars)
        //{
        //    double total = 0;
        //    foreach (Cars car in cars)
        //    {
        //        total += car.Value;
        //    }
        //    return total;

        //}
    }
}
