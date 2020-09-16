using SpooderManCars.Data;
using SpooderManCars.Models.ManufacturerModels;
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
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public RacingService()
        {
           
        }

        public async Task<bool> CreateATeam(RacingCreate model)
        {
            var entity = new Racing()
            {
                BasedOutOF = model.BasedOutOF,
                Drivers = model.Drivers,
                RaceEvent = model.RaceEvent,
                TeamName = model.TeamName,
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
            using (var ctx = new ApplicationDbContext())
            {
                var racingList = await ctx.Racings.Select(
                r =>
                new RacingItem
                {
                    Id = r.Id,
                    BasedOutOF = r.BasedOutOF,
                    Drivers = r.Drivers,
                    Manufacturer = new ManufacturerDetail
                    {
                        Id = r.Manufacturer.Id,
                        CompanyName = r.Manufacturer.CompanyName,
                        Locations = r.Manufacturer.Locations,
                        Founded = r.Manufacturer.Founded
                    },
                    ManufacturerID = r.ManufacturerID,
                    RaceEvent = r.RaceEvent,
                    TeamName = r.TeamName,
                }).ToListAsync();

            return racingList;
            }
        }

        public async Task<RacingItem> GetTeamById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Racings.SingleAsync(r => r.Id == id);
                return
                    new RacingItem
                    {
                        Id = entity.Id,
                        BasedOutOF = entity.BasedOutOF,
                        Drivers = entity.Drivers,
                        Manufacturer = new ManufacturerDetail
                        {
                            Id = entity.Manufacturer.Id,
                            CompanyName = entity.Manufacturer.CompanyName,
                            Locations = entity.Manufacturer.Locations,
                            Founded = entity.Manufacturer.Founded
                        },
                        ManufacturerID = entity.ManufacturerID,
                        RaceEvent = entity.RaceEvent,
                        TeamName = entity.TeamName,
                    };
            }
        }

        public async Task<bool> UpdateTeam(RacingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Racings.Single(r => r.Id == model.Id);

                entity.BasedOutOF = model.BasedOutOF;
                entity.Drivers = model.Drivers;
                entity.RaceEvent = model.RaceEvent;
                entity.TeamName = model.TeamName;

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
