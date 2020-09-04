using Microsoft.AspNet.Identity;
using SpooderManCars.Models;
using SpooderManCars.Models.GarageModels;
using SpooderManCars.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpooderManCars.WebApi.Controllers
{
    [Authorize]
    public class GarageController : ApiController
    {
        private GarageService CreateGarageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var garageService = new GarageService(userId);
            return garageService;
        }

        public IHttpActionResult Post(CreateGarage garage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateGarageService();
            if (!service.CreateGarage(garage))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Get()
        {
            GarageService garageService = CreateGarageService();
            var garages = garageService.GetAllGarages();
            return Ok(garages);
        }

        public IHttpActionResult Get(int id)
        {
            GarageService garageService = CreateGarageService();
            var garage = garageService.GetGarageById(id);
            return Ok(garage);
        }

        public IHttpActionResult Put(GarageEdit garage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateGarageService();

            if (!service.UpdateGarage(garage))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateGarageService();
            if (!service.DeleteGarage(id))
                return InternalServerError();

            return Ok();
        }
    }
}
