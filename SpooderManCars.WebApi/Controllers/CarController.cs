using Microsoft.AspNet.Identity;
using SpooderManCars.Data;
using SpooderManCars.Models.CarModels;
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
    public class CarController : ApiController
    {
        private readonly ApplicationDbContext _content = new ApplicationDbContext();

        private CarService CreateCarService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var raceService = new CarService(userId);
            return raceService;
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTeams()
        {
            List<Car> racings = await _content.Cars.ToListAsync();
            return Ok(racings);
        }
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(CarCreate race)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCarService();


            await service.CreateATeam(race);
            await _content.SaveChangesAsync();
            return Ok(race);


        }
        [HttpGet]
        public async Task<IHttpActionResult> GetCars(int id)
        {
            var service = CreateCarService();

            List<Car> racings = await _content.Cars.ToListAsync();

            foreach (Car item in racings)
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
        public async Task<IHttpActionResult> UpdateTeams([FromUri] int id, [FromBody] Car model)
        {
            if (ModelState.IsValid)
            {
                Car racing = await _content.Cars.FindAsync(id);
                var service = CreateCarService();


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
            Car racing = await _content.Cars.FindAsync(id);
            var service = CreateCarService();
            if (racing != null)
            {
                await service.DeleteTeam(id);
            }

            if (await _content.SaveChangesAsync() == 1)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
