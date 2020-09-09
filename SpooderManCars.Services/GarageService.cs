using SpooderManCars.Data;
using SpooderManCars.Models;
using SpooderManCars.Models.GarageModels;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool CreateGarage(CreateGarage model)
        {
            var entity = new Garage()
            {
                CollectorId = _userId,
                Name = model.Name,
                Location = model.Location,
            };

            _context.Garages.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<GarageItem> GetAllGarages()
        {
            var query = _context.Garages.Where(g => g.CollectorId == _userId)
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
                });
            return query.ToArray();
        }

        public IEnumerable<GarageItem> GetGarageById(int id)
        {
            var query = _context.Garages.Where(g => g.CollectorId == _userId && g.Id == id)
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
                }) ;
            return query.ToArray();
        }

        public bool UpdateGarage(GarageEdit model)
        {
            var entity = _context.Garages.Single(g => g.Id == model.Id && g.CollectorId == _userId);
            entity.Location = model.Location;
            entity.Name = model.Name;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteGarage(int garageId)
        {
            var entity = _context.Garages.Single(g => g.Id == garageId && g.CollectorId == _userId);
            _context.Garages.Remove(entity);

            return _context.SaveChanges() == 1;
        }
    }
}
