using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MvcWebApi.Services;

namespace MvcWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        private readonly IFactorialService _service;

        public ValuesController(IFactorialService service)
        {
            _service = service;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            string userid = RequestContext.Principal.Identity.GetUserName();

            return new string[] { "UserId", userid };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        public struct MyParam
        {
            public int extra { get; set; } 
        }

        [HttpGet]
        [Route("factorial/{number}")]
        public int GetFactorial(int number)
        {
            int result = 0;

            if (number > 0)
            {
                result = number;
                do
                {
                    result = _service.MultiplyNumbers(result, number - 1);
                    number--;
                } while (number > 1);
            }
            return result;
        }

        public IHttpActionResult GetEmpty()
        {
            //return Ok(2);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
