using SpooderManCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Services
{
    public class ManufacturerService
    {
            public bool CreateManufacturer(Manufacturer model)
            {
                var entity = new Manufacturer()
                {
                    CompanyName = model.CompanyName,
                    Locations = model.Locations,
                    Founded = model.Founded
                };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Manufacturers.Add(entity);

                    return ctx.SaveChanges() == 1;
                }
            }

            public List<Manufacturer> GetManufacturer()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query = ctx.Manufacturers.Select(
                        e =>
                        new Manufacturer
                        {
                            Id = e.Id,
                            CompanyName = e.CompanyName,
                            Locations = e.Locations,
                            Cars = e.Cars,
                            Founded = e.Founded
                        }
                        );
                    return query.ToList();
                }
            }

           
        

    }
}
