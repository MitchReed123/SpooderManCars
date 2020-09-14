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
    public class CarService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly Guid _userId;
        public CarService(Guid UserId)
        {
            _userId = UserId;
        }

        public async Task<bool> CreateACar(CarCreate model)
        {
            var entity = new Car()
            {
                OwnerID = _userId,
                ManufacturerId = model.ManufacturerId,
                GarageId = model.GarageId,
                Make = model.Make,
                Model = model.Model,
                Year = model.Year,
                CarType = model.CarType,
                Transmission = model.Transmission,
                CarValue = model.CarValue
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cars.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<CarItem>> GetCars()
        {
                var carList = await _context.Cars.Where(r => r.OwnerID == _userId).Select(entity => new CarItem
                {
                    Id = entity.Id,
                    OwnerID = _userId,
                    ManufacturerId = entity.ManufacturerId,
                    Manufacturer = new ManufacturerDetail
                    {
                        Id = entity.Manufacturer.Id,
                        CompanyName = entity.Manufacturer.CompanyName,
                        Locations = entity.Manufacturer.Locations,
                        Founded = entity.Manufacturer.Founded
                    },
                    GarageId = entity.GarageId,
                    Garage = new GarageSimpleItem
                    {
                        Id = entity.Garage.Id,
                        Name = entity.Garage.Name,
                        Location = entity.Garage.Location
                    },
                    Make = entity.Make,
                    Model = entity.Model,
                    Year = entity.Year,
                    CarType = entity.CarType,
                    Transmission = entity.Transmission,
                    CarValue = entity.CarValue
                }).ToListAsync();
            return carList;
        }

        public async Task<CarItem> GetCarById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Cars.SingleAsync(r => r.Id == id && r.OwnerID == _userId);
                return
                    new CarItem
                    {
                        Id = entity.Id,
                        OwnerID = _userId,
                        ManufacturerId = entity.ManufacturerId,
                        Manufacturer = new ManufacturerDetail
                        {
                            Id = entity.Manufacturer.Id,
                            CompanyName = entity.Manufacturer.CompanyName,
                            Locations = entity.Manufacturer.Locations,
                            Founded = entity.Manufacturer.Founded
                        },
                        GarageId = entity.GarageId,
                        Garage = new GarageSimpleItem
                        {
                            Id = entity.Garage.Id,
                            Name = entity.Garage.Name,
                            Location = entity.Garage.Location
                        },
                        Make = entity.Make,
                        Model = entity.Model,
                        Year = entity.Year,
                        CarType = entity.CarType,
                        Transmission = entity.Transmission,
                        CarValue = entity.CarValue
                    };
            }
        }

        public async Task<bool> UpdateCar(CarEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Cars.Single(r => r.Id == model.Id && r.OwnerID == _userId);
                entity.ManufacturerId = model.ManufacturerId;
                entity.GarageId = model.GarageId;
                entity.Make = model.Make;
                entity.Model = model.Model;
                entity.Year = model.Year;
                entity.CarType = model.CarType;
                entity.Transmission = model.Transmission;
                entity.CarValue = model.CarValue;
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteCar(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Cars.Single(r => r.Id == id && r.OwnerID == _userId);
                ctx.Cars.Remove(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
