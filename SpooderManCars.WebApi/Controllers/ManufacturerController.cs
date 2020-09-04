using Microsoft.AspNet.Identity;
using SpooderManCars.Data;
using SpooderManCars.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public IHttpActionResult Get()
        {
            ManufacturerService manufacturerService = CreateManufacturerService();
            var manufacturers = manufacturerService.GetManufacturer();
            return Ok(manufacturers);
        }

        public IHttpActionResult Post(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateManufacturerService();

            if (!service.CreateManufacturer(manufacturer))
                return InternalServerError();

            return Ok();
        }

       
    }
}
