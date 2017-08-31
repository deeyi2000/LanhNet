using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Services;

namespace Com.LanhNet.Iot.WepApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class SetController : Controller
    {
        protected IIotApiService _service;

        public SetController(IIotApiService service)
        {
            _service = service;
        }

        // GET api/set/{id}/{cmd}
        [HttpGet("{id}/{cmd}")]
        public string Get(Guid id, string cmd)
        {
            return _service.Set(id, JObject.Parse(cmd)).ToString();
        }

        // POST api/set/{id}
        [HttpPost("{id}")]
        public string Post(Guid id, [FromBody]string cmd)
        {
            return _service.Set(id, JObject.Parse(cmd)).ToString();
        }
    }
}
