using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ListIt_BusinessLogic.Services.Generics;

namespace ListIt_WebAPI.Controllers.Generics
{
    public class GenericController<T, DTO> : ApiController
    where T : class
    where DTO : class
    {
        protected readonly Service<T, DTO> _service;

        public GenericController(Service<T, DTO> service)
        {
            _service = service;
        }

        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }


    }
}