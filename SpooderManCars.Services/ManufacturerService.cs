using SpooderManCars.Data;


using SpooderManCars.Models.CarModels;

using SpooderManCars.Models.ManufacturerModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Services
{
    public class ManufacturerService
    {

        public bool CreateManufacturer(ManufacturerCreate model)
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

        public List<ManufacturerListItem> GetManufacturer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Manufacturers.Select(
                    e =>
                    new ManufacturerListItem
                    {
                        Id = e.Id,
                        CompanyName = e.CompanyName,
                        Locations = e.Locations,
                        Cars = e.Cars.Select(r => new CarItem
                        {
                            Id = r.Id,
                            ManufacturerId = r.ManufacturerId,
                            GarageId = r.GarageId,
                            OwnerID = r.OwnerID,
                            Make = r.Make,
                            Model = r.Model,
                            Year = r.Year,
                            CarType = r.CarType,
                            Transmission = r.Transmission
                        }),
                        Founded = e.Founded
                    }
                    );
                return query.ToList();
            }
        }

        public ManufacturerDetail GetManufacturerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Manufacturers
                        .Single(e => e.Id == id);
                return
                    new ManufacturerDetail
                    {
                        Id = entity.Id,
                        CompanyName = entity.CompanyName
                    };
            }
        }


        public bool UpdateManufacturer(ManufacturerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Manufacturers
                        .Single(e => e.Id == model.Id && e.CompanyName == model.CompanyName);


                entity.Id = model.Id;
                entity.CompanyName = model.CompanyName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteManufacturer(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Manufacturers
                    .Single(e => e.Id == Id && e.CompanyName == e.CompanyName);
                ctx.Manufacturers.Remove(entity);
                return ctx.SaveChanges() == 1;
            }

        }

    }
}





