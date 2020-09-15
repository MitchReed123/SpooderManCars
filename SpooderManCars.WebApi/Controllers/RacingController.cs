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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpooderManCars.WebApi.Controllers
{
    public class RacingController : ApiController
    {
        private readonly ApplicationDbContext _content = new ApplicationDbContext();

        private RacingService CreateRaceService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var raceService = new RacingService();
            return raceService;
        }
        /// <summary>
        /// Looks up all Racing Teams in the database
        /// </summary>
        /// <returns>All Racing Teams in the database with all the properties</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTeams()
        {
            RacingService racingService = CreateRaceService();
            var racings = await racingService.GetRaces();
            return Ok(racings);
        }
        /// <summary>
        /// Requires a Manufacturer to Create a Racing Team
        /// </summary>
        /// <returns>A new object based on all body elements that the user gave to the program</returns>
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
        /// <summary>
        /// View Racing Team information by Id
        /// </summary>
        /// <returns>A specific object by the Id given by the user</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRaces(int id)
        {
            var racing = await CreateRaceService().GetTeamById(id);
            if (racing != null) { return Ok(racing); }
            return NotFound();
        }
        /// <summary>
        /// Updates the Racing Team based on what ID the user gave in the URI and what is in the body
        /// </summary>
        /// <returns>A Updated Object the user submitted</returns>
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTeams([FromUri] int id, [FromBody] RacingEdit model)
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
        /// <summary>
        /// Deletes a Racing team based on what Id is givin in the URI
        /// </summary>
        /// <returns>A deleted Object</returns>
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
