using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ListIt_BusinessLogic.DTO;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel;
using ListIt_WebAPI.Controllers.Generics;

namespace ListIt_WebAPI.Controllers
{
    public class ShopApisController : GenericController<ShopApi, ShopApiDto>
    {
        public ShopApisController() : base(new ShopApiService())
        {

        }

        /* NOT GENERIC IMPLEMENTATION COMMENTED OUT BELOW
        private readonly ShopApiService _shopApiService = new ShopApiService();

        // GET api/values
        public IHttpActionResult GetAll()
        {
            return Ok(_shopApiService.GetAll());
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var shopApiDto = _shopApiService.Get(id);
            if (shopApiDto == null) return NotFound();
            return Ok(shopApiDto);
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]ShopApiDto shopApiDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            _shopApiService.Create(shopApiDto);
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody]ShopApiDto shopApiDto)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            if (shopApiDto.Id != 0 && shopApiDto.Id != id) 
                return BadRequest("Given ID and path variable ID differ");
            
            shopApiDto.Id = id;

            try
            {
                _shopApiService.Update(shopApiDto);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest("Given ID might not exist in the database. " + e.Message);
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _shopApiService.Delete(id);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
        */
    }
}
