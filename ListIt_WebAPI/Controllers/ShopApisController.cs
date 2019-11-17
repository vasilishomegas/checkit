﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ListIt_BusinessLogic.DTO;
using ListIt_BusinessLogic.Services;

namespace ListIt_WebAPI.Controllers
{
    public class ShopApisController : ApiController
    {
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
            if (shopApiDto.Id != 0 && shopApiDto.Id != id) 
                return BadRequest("Given ID and path variable ID differ");
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            shopApiDto.Id = id;
            if (_shopApiService.Update(shopApiDto)) return Ok();
            return NotFound();
        }

        public IHttpActionResult Delete(int id)
        {
            if (_shopApiService.Delete(id)) return Ok();
            return NotFound();
        }
    }
}