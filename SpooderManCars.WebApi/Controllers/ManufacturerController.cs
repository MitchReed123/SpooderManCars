using Microsoft.AspNet.Identity;
using SpooderManCars.Data;
using SpooderManCars.Models.ManufacturerModels;
using SpooderManCars.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using static SpooderManCars.Services.ManufacturerService;

namespace SpooderManCars.WebApi.Controllers
{
    public class ManufacturerController : ApiController
    {
        private readonly ApplicationDbContext _content = new ApplicationDbContext();
        private ManufacturerService CreateManufacturerService()
        {

            var manufacturerService = new ManufacturerService();
            return manufacturerService;
        }
        /// <summary>
        /// Looks up all Manufacturers in the database
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            ManufacturerService manufacturerService = CreateManufacturerService();
            var manufacturers = await manufacturerService.GetManufacturer();
            return Ok(manufacturers);
        }
        /// <summary>
        /// Creates a Manufacturer 
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(ManufacturerCreate manufacturer)
        {
            var service = CreateManufacturerService();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!(await service.CreateManufacturer(manufacturer)))
                return InternalServerError();

            return Ok(manufacturer);
        }
        /// <summary>
        /// View Manufacturer information by Id
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            ManufacturerService manufacturerService = CreateManufacturerService();
            var manufacturer = await manufacturerService.GetManufacturerById(id);
            return Ok(manufacturer);
        }
        /// <summary>
        /// Updates Manufacturer based on what is in the body.
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> Put(ManufacturerEdit manufacturer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateManufacturerService();

            if (!(await service.UpdateManufacturer(manufacturer)))
                return InternalServerError();

            return Ok(manufacturer);
        }
        /// <summary>
        /// Deletes a Manufacturer based on the ID given
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var service = CreateManufacturerService();
            if (!(await service.DeleteManufacturer(id)))
                return InternalServerError();
            return Ok();
        }




    }
}
