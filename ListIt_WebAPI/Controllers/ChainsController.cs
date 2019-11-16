using ListIt_BusinessLogic.Services;
using ListIt_DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ListIt_WebAPI.Controllers
{
    public class ChainsController : ApiController
    {
        private readonly ChainService _chainService = new ChainService();

        // GET api/values
        public IEnumerable<Chain> Get()
        {
            return _chainService.GetAll();
        }

        // GET api/values/5
        public Chain Get(int id)
        {
            return _chainService.Get(id);
        }

        // POST api/values
        public void Post([FromBody]Chain chain)
        {
            _chainService.Create(chain);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Chain chain)
        {
            if(id == chain.Id) _chainService.Update(chain);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _chainService.Delete(id);
        }
    }
}
