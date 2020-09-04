using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Services
{
    public class ManufacturerServices
    {
        private readonly int _userId;

        public ManufacturerServices(int userId)
        {
            _userId = userId;
        }

        public bool CreateManufacturer(Manufacturer model)
        {
            var entity = new Manufacturer()
            {
                Id = _userId,
                CompanyName = model.CompanyName,
                Locations = model.Locations,
                Cars = model.Cars,
                Founded = model.Founded

            };
         using (var ctx = new ApplicationDbContext())
            {
                ctx.Manufacturers.Add(entity);

                return ctx.SaveChanges() == 0;
            }
        }

        public List<Manufacturer> GetManufacturer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Manufacturers.Where(e => e.Id == _userId).Select(
                    e =>
                    new Manufacturer 
                    {
                        Id = e.Id,
                        CompanyName = e.CompanyName,
                        Locations = e.Locations,
                        Cars = e.Cars,
                        Founded= e.Founded
                    }
                    );
                return query.ToList();
            }
        }
    }
}
