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
                Location = model.Location,
            };

            _context.Garages.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<GarageItem>> GetAllGarages()
        {
            var query = await _context.Garages.Where(g => g.CollectorId == _userId)
                .Select(g => new GarageItem
                {
                    Id = g.Id,
                    Name = g.Name,
                    Location = g.Location,
                    CarCollection = g.CarCollection.Select(c => new CarItem
                    {
                        Id = c.Id,
                        ManufacturerId = c.ManufacturerId,
                        GarageId = c.GarageId,
                        OwnerID = c.OwnerID,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year,
                        CarType = c.CarType,
                        Transmission = c.Transmission,
                        CarValue = c.CarValue,
                    })
                }).ToArrayAsync();
            return query;
        }

        public async Task<GarageItem> GetGarageById(int id)
        {
            var query = await _context.Garages.SingleAsync(g => g.CollectorId == _userId && g.Id == id);
            return
                new GarageItem
                {
                    Id = g.Id,
                    Name = g.Name,
                    Location = g.Location,
                    CarCollection = g.CarCollection.Select(c => new CarItem
                    {
                        Id = c.Id,
                        ManufacturerId = c.ManufacturerId,
                        GarageId = c.GarageId,
                        OwnerID = c.OwnerID,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year,
                        CarType = c.CarType,
                        Transmission = c.Transmission,
                        CarValue = c.CarValue,
                    })
                }) ;
            return query.ToArray();
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

                    return await _context.SaveChangesAsync() == 1;
                }
            }

        }
    }
}
