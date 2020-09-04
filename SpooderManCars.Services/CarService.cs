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

        public async Task<bool> CreateATeam(CarCreate model)
        {
            var entity = new Car()
            {
                Make = model.Make
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cars.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<List<CarItem>> GetRaces()
        {
            var entityList = await _context.Cars.ToListAsync();

            var racingList = entityList.Select(r => new CarItem
            {

                Make = r.Make
            }).ToList();

            return racingList;
        }

        public async Task<Car> GetTeamById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Cars.SingleAsync(r => r.Id == id);
                return
                    new Car
                    {
                        Make = entity.Make
                    };
            }
        }

        public async Task<bool> UpdateTeam(Car model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Cars.Single(r => r.Id == model.Id);

                entity.Make = model.Make;
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteTeam(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Cars.Single(r => r.Id == id);
                ctx.Cars.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
