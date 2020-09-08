using Microsoft.AspNet.Identity;
using SpooderManCars.Data;
using SpooderManCars.Models.RacingModels;
using SpooderManCars.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpooderManCars.WebApi.Controllers
{
    public class RacingController : ApiController
    {
        private readonly ApplicationDbContext _content = new ApplicationDbContext();

        private RacingService CreateRaceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var raceService = new RacingService(userId);
            return raceService;
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTeams()
        {
            List<Racing> racings = await _content.Racings.ToListAsync();
            return Ok(racings);
        }
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(RacingCreate race)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRaceService();


            await service.CreateATeam(race);
            await _content.SaveChangesAsync();
            return Ok(race);


        }
        [HttpGet]
        public async Task<IHttpActionResult> GetRaces(int id)
        {
            var service = CreateRaceService();

            List<Racing> racings = await _content.Racings.ToListAsync();

            foreach (Racing item in racings)
            {
                if (item.Id == id)
                {
                    await service.GetTeamById(id);
                    return Ok();
                }
            }

            return NotFound();

        }
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTeams([FromUri] int id, [FromBody] Racing model)
        {
            if (ModelState.IsValid)
            {
                Racing racing = await _content.Racings.FindAsync(id);
                var service = CreateRaceService();


                if (racing != null)
                {
                    await service.UpdateTeam(model);
                    return Ok(model);
                }

            }
            return BadRequest();

        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTeams([FromUri] int id)
        {
            Racing racing = await _content.Racings.FindAsync(id);
            var service = CreateRaceService();
            if (racing != null)
            {
                await service.DeleteTeam(id);
                return Ok();
            }

            return InternalServerError();
        }
    }
}
