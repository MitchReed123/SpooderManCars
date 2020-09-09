using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
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
        private CarService CreateCarService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var carService = new CarService(userId);
            return carService;
        }
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(CarCreate car)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await CreateCarService().CreateACar(car);
            return Ok(car);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetCars()
        {
            var cars = await CreateCarService().GetCars();
            return Ok(cars);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetCarById(int id)
        {
            var car = await CreateCarService().GetCarById(id);
            if (car != null) { return Ok(car); }

            return NotFound();

        }
        [HttpPut]
        public async Task<IHttpActionResult> PutCar(CarEdit car)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (!(await CreateCarService().UpdateCar(car)))
                return InternalServerError();
            return Ok();

        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCar(int id)
        {
            if (!(await CreateCarService().DeleteCar(id)))
            { return InternalServerError();}
            return Ok();
        }
    }
}
