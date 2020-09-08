using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
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
        private readonly ApplicationDbContext _context;
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
                Transmission = model.Transmission
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cars.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<CarItem>> GetCars()
        {
            var entityList = await _context.Cars.ToListAsync();

            var carList = entityList.Where(r => r.OwnerID == _userId).Select(r => new CarItem
            {
                Id = r.Id,
                ManufacturerId = r.ManufacturerId,
                Manufacturer = r.Manufacturer,
                GarageId = r.GarageId,
                Garage = r.Garage,
                OwnerID = r.OwnerID,
                Make = r.Make,
                Model = r.Model,
                Year = r.Year,
                CarType = r.CarType,
                Transmission = r.Transmission
            }).ToList();

            return carList;
        }

        public async Task<Car> GetCarById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Cars.SingleAsync(r => r.Id == id);
                return
                    new Car
                    {
                        Id = entity.Id,
                        ManufacturerId = entity.ManufacturerId,
                        Manufacturer = entity.Manufacturer,
                        GarageId = entity.GarageId,
                        Garage = entity.Garage,
                        OwnerID = entity.OwnerID,
                        Make = entity.Make,
                        Model = entity.Model,
                        Year = entity.Year,
                        CarType = entity.CarType,
                        Transmission = entity.Transmission
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
