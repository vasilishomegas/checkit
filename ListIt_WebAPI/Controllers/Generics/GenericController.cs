using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ListIt_BusinessLogic.DTO.Interfaces;
using ListIt_BusinessLogic.Services.Generics;

namespace ListIt_WebAPI.Controllers.Generics
{
    public abstract class GenericController<T, IDTO> : ApiController
    where T : class
    where IDTO : class
    {
        protected readonly Service<T, IDTO> _service;

        protected GenericController(Service<T, IDTO> service)
        {
            _service = service;
        }

        public virtual IHttpActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        public virtual IHttpActionResult Get(int id)
        {
            var dto = _service.Get(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        public virtual IHttpActionResult Post([FromBody] IDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            _service.Create(dto);
            return Ok();
        }

        public virtual IHttpActionResult Put(int id, [FromBody] IDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            if (((IDto)dto).Id != 0 && ((IDto)dto).Id != id)
                return BadRequest("Given ID and path variable ID differ");

            ((IDto)dto).Id = id;

            try
            {
                _service.Update(dto);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest("Given ID might not exist in the database. " + e.Message);
            }

            return Ok();
        }
        public virtual IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}