using SpooderManCars.Data;
using SpooderManCars.Models.RacingModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Services
{
    public class RacingService
    {
        private readonly ApplicationDbContext _context;
        private readonly Guid _userId;
        public RacingService(Guid UserId)
        {
            _userId = UserId;
        }

        public async Task<bool> CreateATeam(RacingCreate model)
        {
            var entity = new Racing()
            {
                BasedOutOF = model.BasedOutOF,
                Drivers = model.Drivers,
                Manufacturer = model.Manufacturer,
                RaceEvent = model.RaceEvent,
                TeamName = model.TeamName,
                Victories = model.Victories,
                ManufacturerID = model.ManufacturerID
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Racings.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<List<RacingItem>> GetRaces()
        {
            var entityList = await _context.Racings.ToListAsync();

            var racingList = entityList.Select(r => new RacingItem
            {
                Id = r.Id,
                BasedOutOF = r.BasedOutOF,
                Drivers = r.Drivers,
                Manufacturer = r.Manufacturer,
                ManufacturerID = r.ManufacturerID,
                RaceEvent = r.RaceEvent,
                TeamName = r.TeamName,
                Victories = r.Victories,
            }).ToList();

            return racingList;
        }

        public async Task<Racing> GetTeamById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Racings.SingleAsync(r => r.Id == id);
                return
                    new Racing
                    {
                        BasedOutOF = entity.BasedOutOF,
                        Drivers = entity.Drivers,
                        Id = entity.Id,
                        ManufacturerID = entity.ManufacturerID,
                        RaceEvent = entity.RaceEvent,
                        TeamName = entity.TeamName,
                        Victories = entity.Victories
                    };
            }
        }

        public async Task<bool> UpdateTeam(Racing model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Racings.Single(r => r.Id == model.Id);

                entity.BasedOutOF = model.BasedOutOF;
                entity.Drivers = model.Drivers;
                entity.RaceEvent = model.RaceEvent;
                entity.TeamName = model.TeamName;
                entity.Victories = model.Victories;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteTeam(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Racings.Single(r => r.Id == id);
                ctx.Racings.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
