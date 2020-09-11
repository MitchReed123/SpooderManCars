using SpooderManCars.Data;
using SpooderManCars.Models;
using SpooderManCars.Models.GarageModels;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.CodeDom;

namespace SpooderManCars.Services
{
    public class GarageService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly Guid _userId;

        public GarageService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateGarage(CreateGarage model)
        {
            var entity = new Garage()
            {
                CollectorId = _userId,
                Name = model.Name,
                Location = model.Location
            };

            _context.Garages.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }
        public async Task<List<GarageItem>> GetAllGarages()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var queryT = await ctx.Garages.Select(
                    e =>
                    new GarageItem
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Location = e.Location,
                        CarCollection = e.CarCollection.Select(r => new CarItem
                        {
                            Id = r.Id,
                            ManufacturerId = r.ManufacturerId,
                            GarageId = r.GarageId,
                            OwnerID = r.OwnerID,
                            Make = r.Make,
                            Model = r.Model,
                            Year = r.Year,
                            CarType = r.CarType,
                            Transmission = r.Transmission,
                            CarValue = r.CarValue,
                        }),
                    }
                    ).ToListAsync();
                return queryT;
            }
        }

        public IEnumerable<CarItem> GetCarItem(IEnumerable<Car> cars)
        {
            return cars.Select(c => new CarItem
            {
                Id = c.Id,
                ManufacturerId = c.ManufacturerId,
                Manufacturer = c.Manufacturer,
                GarageId = c.GarageId,
                Garage = c.Garage,
                OwnerID = c.OwnerID,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                CarType = c.CarType,
                Transmission = c.Transmission,
                CarValue = c.CarValue
            });
        }


        public async Task<GarageItem> GetGarageById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                        .Garages
                        .SingleAsync(e => e.Id == id && e.CollectorId == _userId);
              
                var garageItem =  new GarageItem
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Location = entity.Location,
                        CarCollection = entity.CarCollection.Select(r => new CarItem
                        {
                            Id = r.Id,
                            ManufacturerId = r.ManufacturerId,
                            GarageId = r.GarageId,
                            OwnerID = r.OwnerID,
                            Make = r.Make,
                            Model = r.Model,
                            Year = r.Year,
                            CarType = r.CarType,
                            Transmission = r.Transmission,
                            CarValue = r.CarValue,
                        }).ToList()
                    };
                return garageItem;

            }
        }

        public async Task<bool> UpdateGarage(GarageEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Garages.SingleAsync(g => g.Id == model.Id && g.CollectorId == _userId);
                entity.Location = model.Location;
                entity.Name = model.Name;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteGarage(int garageId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                {
                    var entity = ctx.Garages.Single(g => g.Id == garageId && g.CollectorId == _userId);
                    ctx.Garages.Remove(entity);

                    return await ctx.SaveChangesAsync() == 1;
                }
            }

        }
    }
}
