using SpooderManCars.Data;
using SpooderManCars.Models;
using SpooderManCars.Models.GarageModels;
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
                Location = model.Location,
                CollectionValue = model.CollectionValue
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
                    Location = g.Location,
                    CollectionValue = g.CollectionValue,
                    CarCollection = g.CarCollection
                });
            return query.ToArray();
        }

        public IEnumerable<GarageItem> GetGarageById(int id)
        {
            var query = _context.Garages.Where(g => g.CollectorId == _userId && g.Id == id)
                .Select(g => new GarageItem
                {
                    Id = g.Id,
                    Location = g.Location,
                    CollectionValue = g.CollectionValue,
                    CarCollection = g.CarCollection
                });
            return query.ToArray();
        }

        public bool UpdateGarage(GarageEdit model)
        {
            var entity = _context.Garages.Single(g => g.Id == model.Id && g.CollectorId == _userId);
            entity.Location = model.Location;
            entity.CollectionValue = model.CollectionValue;

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
