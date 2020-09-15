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
        /// <summary>
        /// Requires Garage and Manufacturer to add a car to the database.
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(CarCreate car)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await CreateCarService().CreateACar(car);
            return Ok(car);
        }
        /// <summary>
        /// Looks up all cars in the database
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetCars()
        {
            var cars = await CreateCarService().GetCars();
            return Ok(cars);
        }
        /// <summary>
        /// View cars information by Id
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetCarById(int id)
        {
            var car = await CreateCarService().GetCarById(id);
            if (car != null) { return Ok(car); }

            return NotFound();

        }
        /// <summary>
        /// Updates car based on what is in the body.
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> PutCar(CarEdit car)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (!(await CreateCarService().UpdateCar(car)))
                return InternalServerError();
            return Ok(car);

        }
        /// <summary>
        /// Deletes a car based on the ID given
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCar(int id)
        {
            if (!(await CreateCarService().DeleteCar(id)))
            { return InternalServerError(); }
            return Ok();
        }
    }
}
