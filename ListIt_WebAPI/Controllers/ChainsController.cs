using ListIt_BusinessLogic.Services;
using ListIt_DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ListIt_BusinessLogic.DTO;

namespace ListIt_WebAPI.Controllers
{
    public class ChainsController : ApiController
    {
        private readonly ChainService _chainService = new ChainService();

        // GET api/values
        public IHttpActionResult Get()
        {
            return Ok(_chainService.GetAll());
        }

        // GET api/values/5
        public IHttpActionResult GetAll(int id)
        {
            var chain = _chainService.Get(id);
            if (chain == null) return NotFound();
            return Ok(chain);
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]ChainDto chainDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            _chainService.Create(chainDto);
            return Ok();
        }


    }
}