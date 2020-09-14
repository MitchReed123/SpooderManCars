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
using SpooderManCars.Models.ManufacturerModels;

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
                var queryT = await ctx.Garages.Where(r=> r.CollectorId == _userId).Select(
                    e =>
                    new GarageItem
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Location = e.Location,
                        CarCollection = e.CarCollection.Select(r => new CarItem {
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
                        }),
                    }
                    ).ToListAsync();
                return queryT;
            }
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
