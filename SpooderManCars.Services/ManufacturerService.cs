using SpooderManCars.Data;
using SpooderManCars.Models;
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

        public async Task<bool> CreateManufacturer(ManufacturerCreate model)
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

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<List<ManufacturerListItem>> GetManufacturer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await ctx.Manufacturers.Select(
                    e =>
                    new ManufacturerListItem
                    {
                        Id = e.Id,
                        CompanyName = e.CompanyName,
                        Locations = e.Locations,
                        ManufactureredCars = e.ManufactureredCars.Select(r => new CarItem
                        {
                            Id = r.Id,
                            ManufacturerId = r.ManufacturerId,
                            Manufacturer = new ManufacturerDetail
                            {
                                Id = r.Manufacturer.Id,
                                CompanyName = r.Manufacturer.CompanyName,
                                Locations = r.Manufacturer.Locations,
                                Founded = r.Manufacturer.Founded
                            },
                            GarageId = r.GarageId,
                            Garage = new GarageSimpleItem
                            {
                                Id = r.Garage.Id,
                                Name = r.Garage.Name,
                                Location = r.Garage.Location
                            },
                            OwnerID = r.OwnerID,
                            Make = r.Make,
                            Model = r.Model,
                            Year = r.Year,
                            CarType = r.CarType,
                            Transmission = r.Transmission,
                            CarValue = r.CarValue
                        }).ToList(),
                        Founded = e.Founded
                    }
                    ).ToListAsync();
                return query;
            }
        }

        public async Task<ManufacturerListItem> GetManufacturerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                        .Manufacturers
                        .SingleAsync(e => e.Id == id);
                return
                    new ManufacturerListItem
                    {
                        Id = entity.Id,
                        CompanyName = entity.CompanyName,
                        Locations = entity.Locations,
                        ManufactureredCars = entity.ManufactureredCars.Select(r => new CarItem
                        {
                            Id = r.Id,
                            ManufacturerId = r.ManufacturerId,
                            Manufacturer = new ManufacturerDetail
                            {
                                Id = r.Manufacturer.Id,
                                CompanyName = r.Manufacturer.CompanyName,
                                Locations = r.Manufacturer.Locations,
                                Founded = r.Manufacturer.Founded
                            },
                            GarageId = r.GarageId,
                            Garage = new GarageSimpleItem
                            {
                                Id = r.Garage.Id,
                                Name = r.Garage.Name,
                                Location = r.Garage.Location
                            },
                            OwnerID = r.OwnerID,
                            Make = r.Make,
                            Model = r.Model,
                            Year = r.Year,
                            CarType = r.CarType,
                            Transmission = r.Transmission,
                            CarValue = r.CarValue
                        }).ToList(),
                        Founded = entity.Founded
                    };
            }
        }


        public async Task<bool> UpdateManufacturer(ManufacturerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Manufacturers
                        .Single(e => e.Id == model.Id);


                entity.Id = model.Id;
                entity.CompanyName = model.CompanyName;
                entity.Locations = model.Locations;
                entity.Founded = model.Founded;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteManufacturer(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Manufacturers
                    .Single(e => e.Id == Id && e.CompanyName == e.CompanyName);
                ctx.Manufacturers.Remove(entity);
                return await ctx.SaveChangesAsync() == 1;
            }

        }

    }
}





