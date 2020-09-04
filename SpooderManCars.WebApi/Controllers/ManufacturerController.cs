using Microsoft.AspNet.Identity;
using SpooderManCars.Data;
using SpooderManCars.Models;
using SpooderManCars.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpooderManCars.WebApi.Controllers
{
    public class ManufacturerController : ApiController
    {
        private ManufacturerServices CreateManufacturerService()
        {
            var manufacturerId = int.Parse(User.Identity.GetUserId());
            var manufacturerService = new ManufacturerServices(manufacturerId);
            return manufacturerService;
        }
        
        public IHttpActionResult Get()
        {
            ManufacturerServices manufacturerService = CreateManufacturerService();
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
