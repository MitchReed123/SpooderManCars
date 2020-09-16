using Microsoft.AspNet.Identity;
using SpooderManCars.Data;
using SpooderManCars.Models;
using SpooderManCars.Models.GarageModels;
using SpooderManCars.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpooderManCars.WebApi.Controllers
{
    [Authorize]
    public class GarageController : ApiController
    {
        //private readonly ApplicationDbContext _content = new ApplicationDbContext();
        private GarageService CreateGarageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var garageService = new GarageService(userId);
            return garageService;
        }
        /// <summary>
        /// Requires Name/Location to Create a Garage
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> Post(CreateGarage garage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGarageService();
            if (!(await service.CreateGarage(garage)))
                return InternalServerError();

            return Ok(garage);
        }
        /// <summary>
        /// Looks up all Garages in the database
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            GarageService garageService = CreateGarageService();
            var garages = await garageService.GetAllGarages();
            return Ok(garages);
        }
        /// <summary>
        /// View Garage information by Id
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            GarageService garageService = CreateGarageService();
            var garage = await garageService.GetGarageById(id);
            return Ok(garage);
        }
        /// <summary>
        /// Updates Garage based on what is in the body.
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> Put(GarageEdit garage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGarageService();

            if (!(await service.UpdateGarage(garage)))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// Deletes a Garage based on the ID given
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var service = CreateGarageService();
            if (!(await service.DeleteGarage(id)))
                return InternalServerError();

            return Ok();
        }
    }
}
