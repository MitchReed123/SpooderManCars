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

        public async Task<IHttpActionResult> Get()
        {
            ManufacturerService manufacturerService = CreateManufacturerService();
            var manufacturers = await manufacturerService.GetManufacturer();
            return Ok(manufacturers);
        }

        public async Task<IHttpActionResult> PostAsync(ManufacturerCreate manufacturer)
        {
            var service = CreateManufacturerService();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!(await service.CreateManufacturer(manufacturer)))
                return InternalServerError();

            return Ok(manufacturer);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            ManufacturerService manufacturerService = CreateManufacturerService();
            var manufacturer = await manufacturerService.GetManufacturerById(id);
            return Ok(manufacturer);
        }

        public async Task<IHttpActionResult> Put(ManufacturerEdit manufacturer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateManufacturerService();

            if (!(await service.UpdateManufacturer(manufacturer)))
                return InternalServerError();

            return Ok(manufacturer);
        }

        public async Task<IHttpActionResult> Delete (int id)
        {
            var service = CreateManufacturerService();
            if (!(await service.DeleteManufacturer(id)))
                return InternalServerError();
            return Ok();
        }

        

       
    }
}
